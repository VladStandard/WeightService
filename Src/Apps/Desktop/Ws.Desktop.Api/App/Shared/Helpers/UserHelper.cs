using System.Security.Claims;

namespace Ws.Desktop.Api.App.Shared.Helpers;

public sealed class UserHelper(IHttpContextAccessor httpContextAccessor)
{
    #region Private

    public Guid UserId => Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid result)
        ? result : Guid.Empty;

    private ClaimsPrincipal User => httpContextAccessor.HttpContext!.User;

    #endregion
}