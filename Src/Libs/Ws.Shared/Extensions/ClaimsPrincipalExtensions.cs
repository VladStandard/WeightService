using System.Security.Claims;

namespace Ws.Shared.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetGuidFromClaim(this ClaimsPrincipal principal, string claimType) =>
        Guid.TryParse(principal.FindFirst(claimType)?.Value, out Guid result) ? result : Guid.Empty;
}