using Ws.Tablet.Models.Features.Users;

namespace Ws.Tablet.Api.App.Features.Users.Common;

public interface IUserService
{
    #region Queries

    UserDto GetByCode(string code);

    #endregion
}