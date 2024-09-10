using System.Security.Claims;
using System.Text.Json;

namespace DeviceControl.Source.Shared.Auth.Extensions;

public abstract class ClaimsMapping
{
    private static Dictionary<string, string> ClaimsTypeMap { get; } = new()
    {
        { ClaimTypes.Name, "preferred_username" },
        { ClaimTypes.Surname, "family_name" },
        { ClaimTypes.GivenName, "given_name" },
        { ClaimTypes.Email, "email" },
        { ClaimTypes.NameIdentifier, "sub" }
    };

    public static void MapJwtClaims(Dictionary<string, string> claimsDict, ClaimsIdentity claimsIdentity, string clientId)
    {
        foreach (KeyValuePair<string, string> claim in ClaimsTypeMap)
            if (claimsDict.TryGetValue(claim.Value, out string? value))
                claimsIdentity.AddClaim(new(claim.Key, value));

        if (!claimsDict.TryGetValue("resource_access", out string? resourceAccess)) return;

        using JsonDocument document = JsonDocument.Parse(resourceAccess);
        if (!document.RootElement.TryGetProperty(clientId, out JsonElement clientElement)) return;
        if (!clientElement.TryGetProperty("roles", out JsonElement rolesElement)) return;

        foreach (JsonElement roleElement in rolesElement.EnumerateArray())
            claimsIdentity.AddClaim(new(ClaimTypes.Role, roleElement.ToString()));
    }
}