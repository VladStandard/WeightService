using System.ComponentModel;
using Ws.Desktop.Api.App.Features.Pallets.Common;
using Ws.Desktop.Models.Features.Pallets.Input;
using Ws.Desktop.Models.Features.Pallets.Output;

namespace Ws.Desktop.Api.App.Features.Pallets;

[ApiController]
[Authorize(PolicyEnum.Pc)]
[Route(ApiEndpoints.Pallets)]
public sealed class PalletController(IPalletApiService palletApiService) : ControllerBase
{
    #region Queries

    [HttpGet]
    public List<PalletInfo> GetByDate(
        [FromQuery, DefaultValue(typeof(DateTime), "0001-01-01T00:00:00")] DateTime startDt,
        [FromQuery, DefaultValue(typeof(DateTime), "9999-12-31T23:59:59")] DateTime endDt
    ) => palletApiService.GetAllByDate(startDt, endDt);

    [HttpGet("{number}")]
    public List<PalletInfo> GetByNumber([FromRoute] string number) =>
        palletApiService.GetByNumber(number);

    [HttpGet("{palletId:guid}/labels")]
    public List<LabelInfo> GetLabelsByPallet(Guid palletId) =>
        palletApiService.GetAllZplByPallet(palletId);

    #endregion

    #region Commands

    [HttpPost]
    public async Task<PalletInfo> Create([FromBody] PalletPieceCreateDto dto) =>
        await palletApiService.CreatePiecePallet(dto);

    [HttpDelete("{palletId:guid}")]
    public async Task Delete(Guid palletId) =>
        await palletApiService.Delete(palletId);

    #endregion
}