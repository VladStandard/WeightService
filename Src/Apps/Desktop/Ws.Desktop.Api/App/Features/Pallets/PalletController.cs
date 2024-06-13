using System.ComponentModel;
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
    public ActionResult<List<PalletInfo>> GetByDate(
        [FromRoute] Guid armId,
        [FromQuery, DefaultValue(typeof(DateTime), "0001-01-01T00:00:00")] DateTime startDt,
        [FromQuery, DefaultValue(typeof(DateTime), "9999-12-31T23:59:59")] DateTime endDt
    ) => palletApiService.GetAllByDate(armId, startDt, endDt);

    [HttpGet("{number}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<PalletInfo> Get([FromRoute] Guid armId, uint number) =>
        palletApiService.GetByNumber(armId, number) is { } arm ? Ok(arm) : NotFound();

    [HttpGet("{palletId:guid}/labels")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<PalletInfo>> GetLabelsByPallet([FromRoute] Guid armId, Guid palletId) =>
        Ok(palletApiService.GetAllZplByArm(armId, palletId));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<PalletInfo> Create([FromRoute] Guid armId, [FromBody] PalletPieceCreateDto dto) =>
        Ok(palletApiService.CreatePiecePallet(armId, dto));
}