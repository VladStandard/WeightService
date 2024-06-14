using Microsoft.AspNetCore.Mvc;
using Ws.Desktop.Api.App.Features.Plu.Common;
using Ws.Desktop.Models.Features.Labels.Input;
using Ws.Desktop.Models.Features.Labels.Output;
using Ws.Desktop.Models.Features.Plus.Piece.Output;
using Ws.Desktop.Models.Features.Plus.Weight.Output;

namespace Ws.Desktop.Api.App.Features.Plu;


[ApiController]
[Route("api/arms/{armId:guid}/plu")]
public class PluWeightController(IPluWeightService pluWeightService, IPluPieceService pluPieceService) : ControllerBase
{

    [HttpGet("piece")]
    public ActionResult<List<PluPiece>> GetAllPieceByArm([FromRoute] Guid armId)
        => Ok(pluPieceService.GetAllPieceByArm(armId));

    [HttpGet("weight")]
    public ActionResult<List<PluWeight>> GetAllWeightByArm([FromRoute] Guid armId)
        => Ok(pluWeightService.GetAllWeightByArm(armId));

    [HttpPost("weight/{pluId:guid}/label")]
    public ActionResult<WeightLabel> GenerateLabel([FromRoute] Guid armId, Guid pluId, [FromBody] CreateWeightLabelDto dto)
        => Ok(pluWeightService.GenerateLabel(armId, pluId, dto));
}