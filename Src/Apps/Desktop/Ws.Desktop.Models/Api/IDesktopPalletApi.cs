using Ws.Desktop.Models.Features.Pallets.Input;
using Ws.Desktop.Models.Features.Pallets.Output;

namespace Ws.Desktop.Models.Api;

public interface IDesktopPalletApi
{
    #region Queries

    [Get("/pallets/{number}")]
    Task<PalletInfo[]> GetPalletByNumber(uint number);

    [Get("/pallets")]
    Task<PalletInfo[]> GetPalletsByArm(DateTime? startDt, DateTime? endDt);

    [Get("/pallets/{palletId}/labels")]
    Task<LabelInfo[]> GetPalletLabels(Guid palletId);

    #endregion

    #region Commands

    [Delete("/pallets/{palletId}")]
    Task DeletePallet(Guid palletId);

    [Post("/pallets")]
    Task<PalletInfo> CreatePiecePallet([Body] PalletPieceCreateDto createDto);

    #endregion
}