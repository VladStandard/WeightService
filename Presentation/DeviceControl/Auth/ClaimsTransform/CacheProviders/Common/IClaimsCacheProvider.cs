using System.Security.Claims;

namespace DeviceControl.Auth.ClaimsTransform.CacheProviders.Common;

internal interface IClaimsCacheProvider
{
    Task<List<Claim>> GetUserClaimsAsync(ClaimsPrincipal principal, int cacheMinutes);
    void ClearAllCache();
    void ClearCacheByUserName(string username);
}