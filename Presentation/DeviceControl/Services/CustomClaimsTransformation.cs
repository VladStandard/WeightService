using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Memory;
using Ws.StorageCore.Entities.SchemaScale.Access;

namespace DeviceControl.Services;

public class CustomClaimsTransformation(IMemoryCache cache) : IClaimsTransformation
{
    private SqlAccessRepository AccessRepository { get; } = new();

    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (!cache.TryGetValue(principal.Identity!.Name!, out List<Claim>? userRights))
        {
            userRights = GetUserRightsAsync(principal.Identity.Name!);
            MemoryCacheEntryOptions cacheLifTime = new()
            {
                 AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2),
            };
            cache.Set(principal.Identity.Name!, userRights, cacheLifTime);
        }
          
        List<Claim> existingClaims = principal.Claims.ToList();
        
        ClaimsIdentity newIdentity = new(existingClaims, principal.Identity.AuthenticationType);
        newIdentity.AddClaims(userRights!);
        ClaimsPrincipal newPrincipal = new(newIdentity);

        return Task.FromResult(newPrincipal);
    }


    private List<Claim> GetUserRightsAsync(string username)
    {
        List<Claim> rights = new();
        SqlAccessEntity access = AccessRepository.GetItemByNameOrCreate(username);
        for (int i = access.Rights; i >= 0; --i)
            rights.Add(new(ClaimTypes.Role, $"{i}"));
        return rights;
    }
}