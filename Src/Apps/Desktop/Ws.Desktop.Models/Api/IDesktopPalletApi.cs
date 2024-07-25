using Ws.Desktop.Models.Features.Pallets.Input;
using Ws.Desktop.Models.Features.Pallets.Output;

namespace Ws.Desktop.Models.Api;

public interface IDesktopPalletApi
{
    #region Queries

    [Get("/arms/{armUid}/pallets/{number}")]
    Task<PalletInfo[]> GetPalletByNumber(Guid armUid, uint number);

    [Get("/arms/{armUid}/pallets")]
    Task<PalletInfo[]> GetPalletsByArm(Guid armUid, DateTime? startDt, DateTime? endDt);

    [Get("/arms/{armUid}/pallets/{palletId}/labels")]
    Task<LabelInfo[]> GetPalletLabels(Guid armUid, Guid palletId);

    #endregion

    #region Commands

    [Post("/arms/{armUid}/pallets/{palletId}")]
    Task DeletePallet(Guid armUid, Guid palletId);

    [Post("/arms/{armUid}/pallets")]
    Task<PalletInfo> CreatePiecePallet(Guid armUid, [Body] PalletPieceCreateDto createDto);

    #endregion
}