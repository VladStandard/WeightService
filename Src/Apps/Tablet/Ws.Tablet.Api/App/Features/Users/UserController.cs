using Microsoft.AspNetCore.Mvc;
using Ws.Tablet.Api.App.Features.Users.Common;
using Ws.Tablet.Api.App.Shared;
using Ws.Tablet.Models.Features.Users;

namespace Ws.Tablet.Api.App.Features.Users;

[ApiController]
[Route(ApiEndpoints.Users)]
public sealed class UserController(IUserService userService)
{
    #region Queries

    [HttpGet]
    public UserDto GetUserByCode([FromQuery(Name = "code")] string code) => userService.GetByCode(code);

    #endregion
}