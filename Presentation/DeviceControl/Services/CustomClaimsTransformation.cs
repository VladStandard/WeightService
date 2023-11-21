using Microsoft.AspNetCore.Authentication;

namespace DeviceControl.Services;

public class CustomClaimsTransformation : IClaimsTransformation
{
    private readonly IMemoryCache _cache;
    private SqlAccessRepository AccessRepository { get; } = new();
    
    public CustomClaimsTransformation(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (!_cache.TryGetValue(principal.Identity.Name, out List<Claim>? userRights))
        {
            userRights = GetUserRightsAsync(principal.Identity.Name);
            MemoryCacheEntryOptions cacheLifTime = new()
            {
                 AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2),
            };
            _cache.Set(principal.Identity.Name, userRights, cacheLifTime);
        }
          
        List<Claim> existingClaims = principal.Claims.ToList();
        
        ClaimsIdentity newIdentity = new(existingClaims, principal.Identity.AuthenticationType);
        newIdentity.AddClaims(userRights);
        ClaimsPrincipal newPrincipal = new(newIdentity);

        return Task.FromResult(newPrincipal);
    }
    
    
    public List<Claim> GetUserRightsAsync(string username)
    {
        List<Claim> rights = new();
        SqlAccessEntity access = AccessRepository.GetItemByNameOrCreate(username);
        for (int i = access.Rights; i >= 0; --i)
            rights.Add(new(ClaimTypes.Role, $"{i}"));
        return rights;
    }
}