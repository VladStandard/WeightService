using System.Security.Claims;

namespace DeviceControl2.Source.Shared.Auth;

public interface IUserCacheService
{
    Task<List<Claim>> GetUserRightsAsync(string username);
    void ClearCacheForUser(string username);
    void ClearAllCaches();
    IEnumerable<string> GetCachedUsernames();
}