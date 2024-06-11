using Refit;
using Ws.Desktop.Models.Features.Arms.Output;
using Ws.Desktop.Models.Features.Labels.Input;
using Ws.Desktop.Models.Features.Labels.Output;
using Ws.Desktop.Models.Features.PalletMen;
using Ws.Desktop.Models.Features.Plus.Output;

namespace Ws.Desktop.Models;

public interface IDesktopApi
{
    [Get("/api/arms")]
    Task<ArmValue> GetArmByName([AliasAs("name")] string armName);

    [Get("/api/arms/{armUid}/plu/weight")]
    Task<PluWeight[]> GetPlusByArm(Guid armUid);

    [Get("/api/pallet-men")]
    Task<PalletMan[]> GetPalletMen();

    [Post("/api/arms/{armUid}/plu/weight/{pluUid}/label")]
    Task<WeightLabel> CreatePluWeightLabel(Guid armUid, Guid pluUid, [Body] CreateWeightLabelDto createDto);
}