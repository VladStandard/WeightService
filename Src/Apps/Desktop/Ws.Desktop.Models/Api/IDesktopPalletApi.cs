using Ws.Desktop.Models.Features.Pallets.Input;
using Ws.Desktop.Models.Features.Pallets.Output;

namespace Ws.Desktop.Models.Api;

public interface IDesktopPalletApi
{
    #region Queries

    [Get("/pallets/{number}")]
    Task<PalletInfo[]> GetPalletByNumber(Guid armUid, uint number);

    [Get("/pallets")]
    Task<PalletInfo[]> GetPalletsByArm(Guid armUid, DateTime? startDt, DateTime? endDt);

    [Get("/pallets/{palletId}/labels")]
    Task<LabelInfo[]> GetPalletLabels(Guid armUid, Guid palletId);

    #endregion

    #region Commands

    [Post("/pallets/{palletId}")]
    Task DeletePallet(Guid armUid, Guid palletId);

    [Post("/pallets")]
    Task<PalletInfo> CreatePiecePallet(Guid armUid, [Body] PalletPieceCreateDto createDto);

    #endregion
}