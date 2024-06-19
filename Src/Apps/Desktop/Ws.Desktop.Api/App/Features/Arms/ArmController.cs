using Microsoft.AspNetCore.Mvc;
using Ws.Desktop.Api.App.Features.Arms.Common;
using Ws.Desktop.Models.Features.Arms.Output;

namespace Ws.Desktop.Api.App.Features.Arms;


[ApiController]
[Route("api/arms")]
public class ArmController(IArmService armService) : ControllerBase
{
    #region Queries

    [HttpGet]
    public ActionResult<ArmValue> GetByPcName([FromQuery] string name) =>
        armService.GetByPcName(name) is { } arm ? Ok(arm) : NotFound();

    #endregion
}