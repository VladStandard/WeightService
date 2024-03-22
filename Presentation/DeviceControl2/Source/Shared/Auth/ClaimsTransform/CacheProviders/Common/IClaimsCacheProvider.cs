using System.Security.Claims;

namespace DeviceControl2.Source.Shared.Auth.ClaimsTransform.CacheProviders.Common;

internal interface IClaimsCacheProvider
{
    Task<List<Claim>> GetUserClaimsAsync(ClaimsPrincipal principal, int cacheMinutes);
    void ClearAllCache();
    void ClearCacheByUserName(string username);
}