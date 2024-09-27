using Ws.DeviceControl.Models.Features.Admins.Users.Commands.Update;
using Ws.DeviceControl.Models.Features.Admins.Users.Queries;

namespace Ws.DeviceControl.Models.Api.Admin;

public interface IWebUserApi
{
    #region Queries

    [Get("/users/{uid}")]
    Task<UserDto> GetUserByUid(Guid uid);

    [Get("/users?productionSite={productionSiteUid}")]
    Task<UserDto[]> GetUsersByProductionSite(Guid productionSiteUid);

    #endregion

    #region Commands

    [Delete("/users/{uid}")]
    Task DeleteUser(Guid uid);

    [Post("/users/{uid}")]
    Task<UserDto> SaveOrUpdateUser(Guid uid, [Body] UserUpdateDto updateDto);

    #endregion
}