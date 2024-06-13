using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ws.Desktop.Api.App.Features.Plu.Piece.Common;
using Ws.Desktop.Models.Features.Plus.Piece.Output;

namespace Ws.Desktop.Api.App.Features.Plu.Piece;

[ApiController]
[AllowAnonymous]
[Route("api/arms/{armId:guid}/plu/piece")]
[Consumes(MediaTypeNames.Application.Json)]
public class PluPieceController(IPluPieceService pluPieceService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<List<PluPiece>> GetAllPieceByArm([FromRoute] Guid armId) =>
        Ok(pluPieceService.GetAllPieceByArm(armId));
}