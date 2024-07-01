using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Ws.Desktop.Api.App.Features.Pallets.Common;
using Ws.Desktop.Models.Features.Pallets.Input;
using Ws.Desktop.Models.Features.Pallets.Output;

namespace Ws.Desktop.Api.App.Features.Pallets;

[ApiController]
[Route("api/arms/{armId:guid}/pallets")]
public class PalletController(IPalletApiService palletApiService) : ControllerBase
{
    #region Queries

    [HttpGet]
    public List<PalletInfo> GetByDate(
        [FromRoute] Guid armId,
        [FromQuery, DefaultValue(typeof(DateTime), "0001-01-01T00:00:00")] DateTime startDt,
        [FromQuery, DefaultValue(typeof(DateTime), "9999-12-31T23:59:59")] DateTime endDt
    ) => palletApiService.GetAllByDate(armId, startDt, endDt);

    [HttpGet("{number}")]
    public List<PalletInfo> GetByNumber([FromRoute] Guid armId, uint number) =>
        palletApiService.GetByNumber(armId, number);

    [HttpGet("{palletId:guid}/labels")]
    public List<LabelInfo> GetLabelsByPallet([FromRoute] Guid armId, Guid palletId) =>
        palletApiService.GetAllZplByArm(armId, palletId);

    #endregion

    #region Commands

    [HttpPost]
    public async Task<PalletInfo> Create([FromRoute] Guid armId, [FromBody] PalletPieceCreateDto dto) =>
        await palletApiService.CreatePiecePallet(armId, dto);

    #endregion
}