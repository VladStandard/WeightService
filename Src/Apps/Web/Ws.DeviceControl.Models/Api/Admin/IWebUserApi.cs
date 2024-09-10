using Ws.DeviceControl.Models.Features.Admins.Users.Commands.Update;
using Ws.DeviceControl.Models.Features.Admins.Users.Queries;

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

    [Delete("/users/{uid}")]
    Task DeleteUser(Guid uid);

    [Put("/users/{uid}")]
    Task<UserDto> UpdateUser(Guid uid, [Body] UserUpdateDto updateDto);

    #endregion
}