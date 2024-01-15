using System.Security.Claims;

namespace DeviceControl.Services.Auth.Common;

public interface IUserCacheService
{
    Task<List<Claim>> GetUserRightsAsync(string username);
    void ClearCacheForUser(string username);
    void ClearAllCaches();
    List<string> GetCachedUsernames();
}