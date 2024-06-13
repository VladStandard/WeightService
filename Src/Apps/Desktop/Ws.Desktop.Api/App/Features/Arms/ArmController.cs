using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ws.Desktop.Api.App.Features.Arms.Common;
using Ws.Desktop.Models.Features.Arms.Output;

namespace Ws.Desktop.Api.App.Features.Arms;


[ApiController]
[AllowAnonymous]
[Route("api/arms")]
[Consumes(MediaTypeNames.Application.Json)]
public class ArmController(IArmService armService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ArmValue> Get([FromQuery] string name) =>
        armService.GetByName(name) is { } arm ? Ok(arm) : NotFound();
}