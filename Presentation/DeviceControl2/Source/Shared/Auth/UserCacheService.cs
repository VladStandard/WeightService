// using System.Security.Claims;
// using Microsoft.Extensions.Caching.Memory;
//
// namespace DeviceControl2.Source.Shared.Auth;
//
// public class UserCacheService(IMemoryCache cache, IUserService userService) : IUserCacheService
// {
//     private readonly List<string> _cachedUsernames = [];
//
//     public async Task<List<Claim>> GetUserRightsAsync(string username)
//     {
//         if (cache.TryGetValue(username, out List<Claim>? userRights))
//             if (userRights != null)
//                 return userRights;
//
//         userRights = await GetUserRightsFromRepositoryAsync(username);
//         MemoryCacheEntryOptions cacheLifetime = new()
//         {
//             AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
//         };
//
//         cache.Set(username, userRights, cacheLifetime);
//         _cachedUsernames.Add(username);
//
//         return userRights;
//     }
//
//     public void ClearCacheForUser(string username)
//     {
//         cache.Remove(username);
//         _cachedUsernames.Remove(username);
//     }
//
//     public void ClearAllCaches()
//     {
//         foreach (string username in _cachedUsernames)
//             cache.Remove(username);
//         _cachedUsernames.Clear();
//     }
//
//     public IEnumerable<string> GetCachedUsernames()
//     {
//         return _cachedUsernames;
//     }
//
//     private Task<List<Claim>> GetUserRightsFromRepositoryAsync(string username)
//     {
//         List<Claim> rights = [];
//
//         UserEntity user = userService.GetItemByNameOrCreate(username);
//
//         rights.AddRange(user.Claims.Select(claim => new Claim(ClaimTypes.Role, claim.Name)));
//         return Task.FromResult(rights);
//     }
// }