using System.Security.Claims;

namespace DeviceControl.Auth.Common;

public interface IUserCacheService
{
    Task<List<Claim>> GetUserRightsAsync(string username);
    void ClearCacheForUser(string username);
    void ClearAllCaches();
    IEnumerable<String> GetCachedUsernames();
}