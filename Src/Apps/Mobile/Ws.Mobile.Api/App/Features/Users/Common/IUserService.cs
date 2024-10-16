using Ws.Mobile.Models.Features.Users;

namespace Ws.Mobile.Api.App.Features.Users.Common;

public interface IUserService
{
    #region Queries

    UserDto GetByCode(string code);

    #endregion
}