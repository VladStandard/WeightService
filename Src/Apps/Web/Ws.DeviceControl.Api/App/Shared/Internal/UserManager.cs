using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Ws.DeviceControl.Api.App.Shared.Expressions;

namespace Ws.DeviceControl.Api.App.Shared.Internal;

public class UserManager(
    IHttpContextAccessor httpContextAccessor,
    IAuthorizationService authorizationService,
    WsDbContext dbContext)
{
    #region Private

    private Guid UserId => Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid result)
        ? result : Guid.Empty;
    private ClaimsPrincipal User => httpContextAccessor.HttpContext!.User;

    #endregion

    #region Public

    public Task<ProxyDto?> GetUserProductionSiteAsync() =>
        dbContext.Users
            .Where(i => i.Id == UserId)
            .Select(i => i.ProductionSite)
            .Select(ProductionSiteCommonExpressions.ToProxy)
            .FirstOrDefaultAsync();


    public async Task<bool> ValidatePolicyAsync(string policy) =>
        (await authorizationService.AuthorizeAsync(User, policy)).Succeeded;

    #endregion
}