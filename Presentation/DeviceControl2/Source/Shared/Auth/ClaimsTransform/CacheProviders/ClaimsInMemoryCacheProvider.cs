using System.Security.Claims;
using DeviceControl2.Source.Shared.Auth.ClaimsTransform.CacheProviders.Common;
using Microsoft.Extensions.Caching.Memory;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.User;

namespace DeviceControl2.Source.Shared.Auth.ClaimsTransform.CacheProviders;

internal class ClaimsInMemoryCacheProvider(IMemoryCache cache, IUserService userService) : IClaimsCacheProvider
{
    public Task<List<Claim>> GetUserClaimsAsync(ClaimsPrincipal principal, int cacheMinutes)
    {
        string cacheKey = (principal.Identity!.Name ?? string.Empty).ToLower();
        
        if (cache.TryGetValue(cacheKey, out List<Claim>? claimsCache))
            if (claimsCache != null)
                return Task.FromResult(claimsCache);

        List<Claim> claims = GetUserRightsFromRepository(principal.Identity!.Name!);
        cache.Set(cacheKey, claims, TimeSpan.FromMinutes(cacheMinutes));
        return Task.FromResult(claims);
    }
    
    public void ClearAllCache()
    {
        if (cache is MemoryCache memCache)
            memCache.Clear();
    }
    
    public void ClearCacheByUserName(string username)
    {
        cache.Remove(username.ToLower());
    }
    
    private List<Claim> GetUserRightsFromRepository(string username)
    {
        UserEntity user = userService.GetItemByNameOrCreate(username);
        user.LoginDt = DateTime.Now;
        userService.Update(user);
        return user.Claims.Select(claim => new Claim(ClaimTypes.Role, claim.Name)).ToList();
    }
}