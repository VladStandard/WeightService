using Refit;
using Ws.Desktop.Models.Features.Arms.Output;
using Ws.Desktop.Models.Features.Labels.Input;
using Ws.Desktop.Models.Features.Labels.Output;
using Ws.Desktop.Models.Features.PalletMen;
using Ws.Desktop.Models.Features.Pallets.Input;
using Ws.Desktop.Models.Features.Pallets.Output;
using Ws.Desktop.Models.Features.Pallets.Output;
using Ws.Desktop.Models.Features.Plus.Piece.Output;
using Ws.Desktop.Models.Features.Plus.Weight.Output;

namespace Ws.Desktop.Models;

public interface IDesktopApi
{
    [Get("/api/arms")]
    Task<ArmValue> GetArmByName([AliasAs("name")] string armName);

    [Get("/api/arms/{armUid}/plu/weight")]
    Task<PluWeight[]> GetPlusWeightByArm(Guid armUid);

    [Get("/api/arms/{armUid}/plu/piece")]
    Task<PluPiece[]> GetPlusPieceByArm(Guid armUid);

    [Get("/api/arms/{armUid}/pallets/{palletId}/labels")]
    Task<LabelInfo[]> GetPalletLabels(Guid armUid, Guid palletId);

    [Get("/api/pallet-men")]
    Task<PalletMan[]> GetPalletMen();

    [Get("/api/arms/{armUid}/pallets")]
    Task<PalletInfo[]> GetPalletsByArm(Guid armUid);

    [Post("/api/arms/{armUid}/plu/weight/{pluUid}/label")]
    Task<WeightLabel> CreatePluWeightLabel(Guid armUid, Guid pluUid, [Body] CreateWeightLabelDto createDto);

    [Post("/api/arms/{armUid}/pallets")]
    Task<PalletInfo> CreatePiecePallet(Guid armUid, [Body] PalletPieceCreateDto createDto);
}