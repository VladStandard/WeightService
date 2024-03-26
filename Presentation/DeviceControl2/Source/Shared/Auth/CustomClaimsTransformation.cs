using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace DeviceControl2.Source.Shared.Auth;

public class CustomClaimsTransformation(IUserCacheService userCacheService) : IClaimsTransformation
{
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        List<Claim> userRights = await userCacheService.GetUserRightsAsync(principal.Identity!.Name!);
        if (userRights.Count == 0)
            return principal;

        List<Claim> existingClaims = new(principal.Claims);
        ClaimsIdentity newIdentity = new(existingClaims, principal.Identity.AuthenticationType);
        newIdentity.AddClaims(userRights);

        return new(newIdentity);
    }
}