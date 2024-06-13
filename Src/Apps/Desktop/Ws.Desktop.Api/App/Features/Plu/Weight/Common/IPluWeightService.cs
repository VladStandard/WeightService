using Ws.Desktop.Models.Features.Labels.Input;
using Ws.Desktop.Models.Features.Labels.Output;
using Ws.Desktop.Models.Features.Plus.Weight.Output;

namespace Ws.Desktop.Api.App.Features.Plu.Weight.Common;

public interface IPluWeightService
{
    public List<PluWeight> GetAllWeightByArm(Guid uid);
    public WeightLabel GenerateLabel(Guid armId, Guid pluId, CreateWeightLabelDto dto);
}