using System.Security.Claims;
using DeviceControl2.Source.Shared.Auth.ClaimsTransform.CacheProviders.Common;
using Microsoft.AspNetCore.Authentication;

namespace DeviceControl2.Source.Shared.Auth.ClaimsTransform;

internal class WsClaimsTransformation(IClaimsCacheProvider cacheProvider) : IClaimsTransformation
{
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        List<Claim> userRights = await cacheProvider.GetUserClaimsAsync(principal, 5);
        if (userRights.Count == 0)
            return principal;

        List<Claim> existingClaims = principal.Claims.ToList();
        existingClaims.AddRange(userRights);

        ClaimsIdentity newIdentity = new(existingClaims, principal.Identity?.AuthenticationType);
        return new(newIdentity);
    }
}
