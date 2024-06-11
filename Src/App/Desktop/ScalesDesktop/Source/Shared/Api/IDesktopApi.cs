using Ws.Desktop.Models.Features.Arms.Output;
using Ws.Desktop.Models.Features.Labels.Input;
using Ws.Desktop.Models.Features.Labels.Output;
using Ws.Desktop.Models.Features.PalletMen;
using Ws.Desktop.Models.Features.Plus.Output;

namespace ScalesDesktop.Source.Shared.Api;

public interface IDesktopApi
{
    Task<ArmValue> GetArmByName(string armName);
    Task<PluWeight[]> GetPlusByArm(Guid armUid);
    Task<WeightLabel> CreatePluWeightLabel(Guid armUid, Guid pluUid, CreateWeightLabelDto createDto);
    Task<PalletMan[]> GetPalletMen();
}