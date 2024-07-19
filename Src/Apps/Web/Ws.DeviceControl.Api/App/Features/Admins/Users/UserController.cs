using Ws.DeviceControl.Api.App.Features.Admins.Users.Common;
using Ws.DeviceControl.Models.Dto.Admins.Users.Queries;

namespace Ws.DeviceControl.Api.App.Features.Admins.Users;

[ApiController]
[Route("api/users/")]
public class UserController(IUserService userService)
{
    #region Queries

    [HttpGet]
    public Task<List<UserDto>> GetAllByProductionSite([FromQuery(Name = "productionSite")] Guid productionSiteId) =>
        userService.GetAllByProductionSiteAsync(productionSiteId);

    [HttpGet("{id:guid}")]
    public Task<UserDto> GetById([FromRoute] Guid id) => userService.GetByIdAsync(id);

    #endregion
}