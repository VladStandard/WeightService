using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace DeviceControl.Source.Shared.Auth.Extensions;

public sealed class CookieOidcRefresher(IOptionsMonitor<OpenIdConnectOptions> oidcOptionsMonitor)
{
    private OpenIdConnectProtocolValidator OidcTokenValidator { get; } = new() { RequireNonce = false };

    public async Task ValidateOrRefreshCookieAsync(CookieValidatePrincipalContext validateContext, string oidcScheme)
    {
        string? accessTokenExpirationText = validateContext.Properties.GetTokenValue("expires_at");
        if (!DateTimeOffset.TryParse(accessTokenExpirationText, out DateTimeOffset accessTokenExpiration))
            return;

        OpenIdConnectOptions oidcOptions = oidcOptionsMonitor.Get(oidcScheme);
        DateTimeOffset now = oidcOptions.TimeProvider!.GetUtcNow();
        if (now < accessTokenExpiration - TimeSpan.FromMinutes(1))
            return;

        OpenIdConnectConfiguration? oidcConfiguration = await oidcOptions.ConfigurationManager!.GetConfigurationAsync(validateContext.HttpContext.RequestAborted);
        string tokenEndpoint = oidcConfiguration.TokenEndpoint ?? throw new InvalidOperationException("Cannot refresh cookie. TokenEndpoint missing!");

        using HttpResponseMessage refreshResponse = await oidcOptions.Backchannel.PostAsync(tokenEndpoint,
            new FormUrlEncodedContent(new Dictionary<string, string?>
            {
                ["grant_type"] = "refresh_token",
                ["client_id"] = oidcOptions.ClientId,
                ["client_secret"] = oidcOptions.ClientSecret,
                ["scope"] = string.Join(" ", oidcOptions.Scope),
                ["refresh_token"] = validateContext.Properties.GetTokenValue("refresh_token"),
            }));

        if (!refreshResponse.IsSuccessStatusCode)
        {
            validateContext.RejectPrincipal();
            return;
        }

        string refreshJson = await refreshResponse.Content.ReadAsStringAsync();
        OpenIdConnectMessage message = new(refreshJson);

        TokenValidationParameters? validationParameters = oidcOptions.TokenValidationParameters.Clone();
        if (oidcOptions.ConfigurationManager is BaseConfigurationManager baseConfigurationManager)
            validationParameters.ConfigurationManager = baseConfigurationManager;
        else
        {
            validationParameters.ValidIssuer = oidcConfiguration.Issuer;
            validationParameters.IssuerSigningKeys = oidcConfiguration.SigningKeys;
        }

        TokenValidationResult? validationResult = await oidcOptions.TokenHandler.ValidateTokenAsync(message.IdToken, validationParameters);

        if (!validationResult.IsValid)
        {
            validateContext.RejectPrincipal();
            return;
        }

        JwtSecurityToken? validatedIdToken = JwtSecurityTokenConverter.Convert(validationResult.SecurityToken as JsonWebToken);
        validatedIdToken.Payload["nonce"] = null;

        OidcTokenValidator.ValidateTokenResponse(new()
        {
            ProtocolMessage = message,
            ClientId = oidcOptions.ClientId,
            ValidatedIdToken = validatedIdToken,
        });

        ClaimsIdentity claimsIdentity = new(validationResult.ClaimsIdentity);
        MapKeyCloakRolesToRoleClaims(validatedIdToken, claimsIdentity);

        validateContext.ShouldRenew = true;
        validateContext.ReplacePrincipal(new(claimsIdentity));

        int expiresIn = int.Parse(message.ExpiresIn, NumberStyles.Integer, CultureInfo.InvariantCulture);
        DateTimeOffset expiresAt = now + TimeSpan.FromSeconds(expiresIn);
        validateContext.Properties.StoreTokens([
            new() { Name = "access_token", Value = message.AccessToken },
            new() { Name = "id_token", Value = message.IdToken },
            new() { Name = "refresh_token", Value = message.RefreshToken },
            new() { Name = "token_type", Value = message.TokenType },
            new() { Name = "expires_at", Value = expiresAt.ToString("o", CultureInfo.InvariantCulture) },
        ]);
    }

    private static void MapKeyCloakRolesToRoleClaims(JwtSecurityToken token, ClaimsIdentity claimsIdentity)
    {
        string? userName = token.Claims.FirstOrDefault(x => x.Type == "preferred_username")?.Value;
        if (userName != null && !string.IsNullOrEmpty(userName)) claimsIdentity.AddClaim(new(ClaimTypes.Name, userName));

        string? resourceAccess = token.Claims.FirstOrDefault(x => x.Type == "resource_access")?.Value;
        if (resourceAccess == null) return;

        JObject jsonObject = JObject.Parse(resourceAccess);

        foreach (JProperty property in jsonObject.Properties())
        {
            JToken? roles = property.Value["roles"];
            if (roles == null) continue;
            foreach (string role in roles.ToObject<List<string>>()!)
                claimsIdentity.AddClaim(new(ClaimTypes.Role, role));
        }
    }
}