using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace KeycloakBlazor.Source.Utils;

public abstract class RoleMapping
{
    public static void MapKeyCloakRolesToRoleClaims(UserInformationReceivedContext context)
    {
        if (context.Principal?.Identity is not ClaimsIdentity claimsIdentity) return;

        if (context.User.RootElement.TryGetProperty("preferred_username", out JsonElement username))
            claimsIdentity.AddClaim(new(ClaimTypes.Name, username.ToString()));

        if (context.User.RootElement.TryGetProperty("realm_access", out JsonElement realmAccess)
            && realmAccess.TryGetProperty("roles", out var globalRoles))
            foreach (JsonElement role in globalRoles.EnumerateArray())
                claimsIdentity.AddClaim(new(ClaimTypes.Role, role.ToString()));

        if (context.Options.ClientId == null ||
            !context.User.RootElement.TryGetProperty("resource_access", out JsonElement clientAccess) ||
            !clientAccess.TryGetProperty(context.Options.ClientId, out JsonElement client) ||
            !client.TryGetProperty("roles", out JsonElement clientRoles)) return;
        {
            foreach (JsonElement role in clientRoles.EnumerateArray())
                claimsIdentity.AddClaim(new(ClaimTypes.Role, role.ToString()));
        }
    }
}