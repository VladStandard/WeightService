using System.Security.Claims;
using DeviceControl.Services.Auth.Common;
using Microsoft.Extensions.Caching.Memory;
using Ws.StorageCore.Entities.SchemaRef.Users;

namespace DeviceControl.Services.Auth;

public class UserCacheService(IMemoryCache cache) : IUserCacheService
{
    private readonly SqlUserRepository _userRepository = new();
    private readonly List<string> _cachedUsernames = [];

    public async Task<List<Claim>> GetUserRightsAsync(string username)
    {
        if (cache.TryGetValue(username, out List<Claim>? userRights))
            return userRights ?? [];

        userRights = await GetUserRightsFromRepositoryAsync(username);
        MemoryCacheEntryOptions cacheLifetime = new()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2),
        };

        cache.Set(username, userRights, cacheLifetime);
        _cachedUsernames.Add(username);

        return userRights;
    }

    public void ClearCacheForUser(string username)
    {
        cache.Remove(username);
        _cachedUsernames.Remove(username);
    }

    public void ClearAllCaches()
    {
        foreach (string username in _cachedUsernames)
            cache.Remove(username);
        _cachedUsernames.Clear();
    }

    public List<string> GetCachedUsernames()
    {
        return _cachedUsernames;
    }
    
    private Task<List<Claim>> GetUserRightsFromRepositoryAsync(string username)
    {
        List<Claim> rights = [];
        SqlUserEntity user = _userRepository.GetItemByUsername(username);
        rights.AddRange(user.Claims.Select(claim => new Claim(ClaimTypes.Role, claim.Name)));
        return Task.FromResult(rights);
    }
}