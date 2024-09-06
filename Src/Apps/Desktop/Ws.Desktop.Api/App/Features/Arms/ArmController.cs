using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ws.Desktop.Api.App.Features.Arms.Common;
using Ws.Desktop.Models.Features.Arms.Input;
using Ws.Desktop.Models.Features.Arms.Output;

namespace Ws.Desktop.Api.App.Features.Arms;


[Authorize]
[ApiController]
[Route(RouteUtil.Arms)]
public class ArmController(IArmService armService) : ControllerBase
{
    #region Queries

    [HttpGet]
    public ActionResult<ArmValue> Get() =>
        armService.Get() is { } arm ? Ok(arm) : NotFound();

    #endregion

    #region Commands

    [HttpPost]
    public ActionResult Update([FromBody] UpdateArmDto dto) =>
        armService.Update(dto) ? Ok() : NotFound();

    #endregion
}