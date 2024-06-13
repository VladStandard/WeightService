using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ws.Desktop.Api.App.Features.Pallets.Common;
using Ws.Desktop.Models.Features.Pallets.Input;
using Ws.Desktop.Models.Features.Pallets.Output;

namespace Ws.Desktop.Api.App.Features.Pallets;

[ApiController]
[AllowAnonymous]
[Route("api/arms/{armId:guid}/pallets")]
[Consumes(MediaTypeNames.Application.Json)]
public class PalletController(IPalletApiService palletApiService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<PalletInfo>> GetAllByArm([FromRoute] Guid armId) =>
        Ok(palletApiService.GetAllByArm(armId));

    [HttpGet("{palletId:guid}/labels")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<PalletInfo>> GetAllByArm([FromRoute] Guid armId, Guid palletId) =>
        Ok(palletApiService.GetAllZplByArm(armId, palletId));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<PalletInfo> GetAllByArm([FromRoute] Guid armId, [FromBody] PalletPieceCreateDto dto) =>
        Ok(palletApiService.CreatePiecePallet(armId, dto));
}