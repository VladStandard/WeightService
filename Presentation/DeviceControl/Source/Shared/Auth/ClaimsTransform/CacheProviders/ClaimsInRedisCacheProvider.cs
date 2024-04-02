using System.Security.Claims;
using DeviceControl.Source.Shared.Auth.ClaimsTransform.CacheProviders.Common;

namespace DeviceControl.Source.Shared.Auth.ClaimsTransform.CacheProviders;

public class ClaimsInRedisCacheProvider : IClaimsCacheProvider
{
    public Task<List<Claim>> GetUserClaimsAsync(ClaimsPrincipal principal, int cacheMinutes)
    {
        throw new NotImplementedException();
    }

    public void ClearAllCache()
    {
        throw new NotImplementedException();
    }

    public void ClearCacheByUserName(String username)
    {
        throw new NotImplementedException();
    }
}