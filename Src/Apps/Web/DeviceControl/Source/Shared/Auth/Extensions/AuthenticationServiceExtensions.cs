using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace DeviceControl.Source.Shared.Auth.Extensions;

public static class AuthenticationServiceExtensions
{
    public static IServiceCollection ConfigureKeycloakAuthorization(this IServiceCollection services, IConfigurationSection oidcConfiguration, string oidcScheme)
    {
        services.AddAuthentication(oidcScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
            {
                opt.ExpireTimeSpan = TimeSpan.FromDays(30);
                opt.SlidingExpiration = true;

                opt.Events.OnSigningIn = context =>
                {
                    context.Properties.IsPersistent = true;
                    context.Properties.ExpiresUtc = DateTimeOffset.UtcNow.Add(opt.ExpireTimeSpan);
                    return Task.CompletedTask;
                };

                opt.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.Redirect(RouteUtils.Home);
                    return Task.CompletedTask;
                };
            })
            .AddOpenIdConnect(oidcScheme, options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.SignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                options.Authority = $"{oidcConfiguration.GetValue<string>("Authority")}/realms/{oidcConfiguration.GetValue<string>("Realm")}";
                options.ClientId = oidcConfiguration.GetValue<string>("ClientId");
                options.ClientSecret = oidcConfiguration.GetValue<string>("ClientSecret");
                options.RequireHttpsMetadata = oidcConfiguration.GetValue<bool>("RequireHttpsMetadata");
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.MapInboundClaims = false;

                options.Events = new()
                {
                    OnUserInformationReceived = context =>
                    {
                        ClaimsIdentity? claimsIdentity = context.Principal?.Identity as ClaimsIdentity;
                        string clientId = context.Options.ClientId ?? "";
                        Dictionary<string, string> claimsDict = context.User.RootElement.EnumerateObject()
                            .ToDictionary(claim => claim.Name, claim => claim.Value.ToString());
                        if (string.IsNullOrWhiteSpace(clientId) || claimsIdentity == null) return Task.CompletedTask;

                        ClaimsMapping.MapJwtClaims(claimsDict, claimsIdentity, clientId);
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddHttpContextAccessor();
        services.ConfigureCookieOidcRefresh(CookieAuthenticationDefaults.AuthenticationScheme, oidcScheme);
        services.AddAuthorization(PolicyAuthUtils.RegisterAuthorization);
        services.AddScoped<AuthenticationStateProvider, CustomRevalidatingAuthenticationStateProvider>();
        services.AddCascadingAuthenticationState();

        return services;
    }
}