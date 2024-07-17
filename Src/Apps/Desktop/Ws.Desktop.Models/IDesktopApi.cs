using Refit;
using Ws.Desktop.Models.Features.Arms.Input;
using Ws.Desktop.Models.Features.Arms.Output;
using Ws.Desktop.Models.Features.Labels.Input;
using Ws.Desktop.Models.Features.Labels.Output;
using Ws.Desktop.Models.Features.PalletMen;
using Ws.Desktop.Models.Features.Pallets.Input;
using Ws.Desktop.Models.Features.Pallets.Output;
using Ws.Desktop.Models.Features.Plus.Piece.Output;
using Ws.Desktop.Models.Features.Plus.Weight.Output;

namespace Ws.Desktop.Models;

public interface IDesktopApi
{
    #region Arms

    [Get("/arms")]
    Task<ArmValue> GetArmByName([AliasAs("name")] string armName);

    [Post("/arms/{armUid}")]
    Task UpdateArm(Guid armUid, [Body] UpdateArmDto updateDto);

    #endregion

    #region Plu Piece

    [Get("/arms/{armUid}/plu/piece")]
    Task<PluPiece[]> GetPlusPieceByArm(Guid armUid);

    #endregion

    #region PalletMan

    [Get("/arms/{armUid}/pallet-men")]
    Task<PalletMan[]> GetPalletMenByArm(Guid armUid);

    #endregion

    #region Pallet

    [Get("/arms/{armUid}/pallets")]
    Task<PalletInfo[]> GetPalletsByArm(Guid armUid, DateTime? startDt, DateTime? endDt);

    [Get("/arms/{armUid}/pallets/{number}")]
    Task<PalletInfo[]> GetPalletByNumber(Guid armUid, uint number);

    [Get("/arms/{armUid}/pallets/{palletId}/labels")]
    Task<LabelInfo[]> GetPalletLabels(Guid armUid, Guid palletId);

    [Post("/arms/{armUid}/pallets")]
    Task<PalletInfo> CreatePiecePallet(Guid armUid, [Body] PalletPieceCreateDto createDto);

    #endregion

    #region Plus Weight

    [Get("/arms/{armUid}/plu/weight")]
    Task<PluWeight[]> GetPlusWeightByArm(Guid armUid);

    [Post("/arms/{armUid}/plu/weight/{pluUid}/label")]
    Task<WeightLabel> CreatePluWeightLabel(Guid armUid, Guid pluUid, [Body] CreateWeightLabelDto createDto);

    # endregion
}