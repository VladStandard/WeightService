using Ws.DeviceControl.Models.Dto.Admins.Users.Commands.Update;
using Ws.DeviceControl.Models.Dto.Admins.Users.Queries;

namespace Ws.DeviceControl.Models.Api.Admin;

public interface IWebUserApi
{
    #region Queries

    [Get("/users/{uid}")]
    Task<UserDto> GetUsersByUid(Guid uid);

    [Get("/users?productionSite={productionSiteUid}")]
    Task<UserDto[]> GetUsersByProductionSite(Guid productionSiteUid);

    #endregion

    #region Commands

    [Post("/users/{uid}")]
    Task<UserDto> UpdateUser(Guid uid, [Body] UserUpdateDto updateDto);

    #endregion
}