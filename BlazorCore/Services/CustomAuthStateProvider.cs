using System.Collections.Generic;
using System.Security.Claims;
using DataCore.Sql.Core.Helpers;
using DataCore.Sql.TableScaleModels.Access;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorCore.Services;


public interface IUserRightsService
{
    Task<List<string>> GetUserRightsAsync(string username);
}

public class UserRightsService : IUserRightsService
{
    private DataAccessHelper DataAccess => DataAccessHelper.Instance;

    public async Task<List<string>> GetUserRightsAsync(string username)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        List<string> rights = new();
        AccessModel? access = DataAccess.GetItemAccessNullable(username);
        if (access is null)
            return rights;
        access.LoginDt = DateTime.Now;
        DataAccess.UpdateForce(access);
        for (int i = access.Rights; i >= 0; --i)
            rights.Add($"{i}");
        return rights;
    }
}

public class CustomAuthStateProvider : AuthenticationStateProvider
{

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRightsService _userRightsService;

    public CustomAuthStateProvider(IHttpContextAccessor httpContextAccessor, IUserRightsService userRightsService)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRightsService = userRightsService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal? user = _httpContextAccessor.HttpContext?.User;

        if (user?.Identity?.IsAuthenticated != true || user.Identity.Name == null )
            return new AuthenticationState(new ClaimsPrincipal());

        ClaimsIdentity claimsIdentity = new (user.Claims, "Windows");
        
        List<string> userRights = await _userRightsService.GetUserRightsAsync(user.Identity.Name);
        
        foreach (string right in userRights)
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, right));
        return new AuthenticationState( new ClaimsPrincipal(claimsIdentity));
    }
}
