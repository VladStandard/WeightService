using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ws.Desktop.Api.App.Features.Plu.Weight.Common;
using Ws.Desktop.Models.Features.Labels.Input;
using Ws.Desktop.Models.Features.Labels.Output;
using Ws.Desktop.Models.Features.Plus.Output;

namespace Ws.Desktop.Api.App.Features.Plu.Weight;


[ApiController]
[AllowAnonymous]
[Route("api/arms/{armId:guid}/plu/weight")]
[Consumes(MediaTypeNames.Application.Json)]
public class PluWeightController(IPluWeightService pluWeightService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<List<PluWeight>> GetAllWeightByArm([FromRoute] Guid armId) =>
        Ok(pluWeightService.GetAllWeightByArm(armId));

    [HttpPost("{pluId:guid}/label")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<WeightLabel> GenerateLabel([FromRoute] Guid armId, Guid pluId, [FromBody] CreateWeightLabelDto dto) =>
        Ok(pluWeightService.GenerateLabel(armId, pluId, dto));
}