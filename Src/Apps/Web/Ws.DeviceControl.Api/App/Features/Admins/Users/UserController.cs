using Refit;
using Ws.DeviceControl.Api.App.Features.Admins.Users.Common;
using Ws.DeviceControl.Models.Features.Admins.Users.Commands.Update;
using Ws.DeviceControl.Models.Features.Admins.Users.Queries;

namespace Ws.DeviceControl.Api.App.Features.Admins.Users;

[ApiController]
[Route(ApiEndpoints.Users)]
[Microsoft.AspNetCore.Authorization.Authorize(PolicyEnum.SeniorSupport)]
public sealed class UserController(IUserService userService)
{
    #region Queries

    [HttpGet]
    public Task<List<UserDto>> GetAllUsers() => userService.GetAllUsers();

    [HttpGet("{id:guid}")]
    public Task<UserDto> GetById([FromRoute] Guid id) => userService.GetByIdAsync(id);

    #endregion

    #region Commands

    [HttpPost("{id:guid}")]
    public Task SaveOrUpdate([FromRoute] Guid id, [Body] UserUpdateDto dto) => userService.SaveOrUpdateUser(id, dto);

    [HttpDelete("{id:guid}")]
    public Task Delete([FromRoute] Guid id) => userService.DeleteAsync(id);

    #endregion
}