using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ws.Desktop.Api.App.Features.Plus.Common;
using Ws.Desktop.Models.Common;
using Ws.Desktop.Models.Features.Plus.Output;

namespace Ws.Desktop.Api.App.Features.Plus;


[ApiController]
[AllowAnonymous]
[Route("api/arms/{armId:guid}/plu")]
[Consumes(MediaTypeNames.Application.Json)]
public class PluController(IPluService pluService) : ControllerBase
{
    [HttpGet("weight")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<OutputDto<List<PluWeight>>> GetAllWeightByArm([FromRoute] Guid armId) =>
        Ok(pluService.GetAllWeightByArm(armId));
}