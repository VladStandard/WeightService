using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Caching.Memory;
using WsDataCore.Enums;
using WsStorageCore.Helpers;
using WsStorageCore.TableScaleModels.Access;

namespace DeviceControl.Services;


public interface IUserRightsService
{
    Task<List<string>> GetUserRightsAsync(string username);
}

public class UserRightsService : IUserRightsService
{
    private WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;

    public async Task<List<string>> GetUserRightsAsync(string username)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        List<string> rights = new();
        WsSqlAccessModel? access = ContextManager.ContextItem.GetItemAccessNullable(username);
        if (access == null)
        {
            access = new WsSqlAccessModel
            {
                LoginDt = DateTime.Now,
                Name = username,
                Rights = (byte)WsEnumAccessRights.None
            };
            ContextManager.AccessItem.Save(access);
        }
        else
        {
            access.LoginDt = DateTime.Now;
            ContextManager.AccessItem.Update(access);
        }
        for (int i = access.Rights; i >= 0; --i)
            rights.Add($"{i}");
        return rights;
    }
}

public class CustomAuthStateProvider : AuthenticationStateProvider
{

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRightsService _userRightsService;
    private readonly IMemoryCache _cache;

    public CustomAuthStateProvider(IHttpContextAccessor httpContextAccessor, IUserRightsService userRightsService, IMemoryCache cache)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRightsService = userRightsService;
        _cache = cache;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal? user = _httpContextAccessor.HttpContext?.User;

        if (user?.Identity?.IsAuthenticated != true || user.Identity.Name == null)
            return new AuthenticationState(new ClaimsPrincipal());

        ClaimsIdentity claimsIdentity = new(user.Claims, "Windows");

        List<string> userRights;

        if (!_cache.TryGetValue(user.Identity.Name, out userRights))
        {
            userRights = await _userRightsService.GetUserRightsAsync(user.Identity.Name);
            MemoryCacheEntryOptions cacheLifTime = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2),
            };
            _cache.Set(user.Identity.Name, userRights, cacheLifTime);
        }
        foreach (string right in userRights)
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, right));
        return new AuthenticationState(new ClaimsPrincipal(claimsIdentity));
    }
}