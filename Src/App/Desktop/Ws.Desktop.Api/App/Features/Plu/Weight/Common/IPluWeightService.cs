using Ws.Desktop.Models.Common;
using Ws.Desktop.Models.Features.Labels.Input;
using Ws.Desktop.Models.Features.Labels.Output;
using Ws.Desktop.Models.Features.Plus.Output;

namespace Ws.Desktop.Api.App.Features.Plu.Weight.Common;

public interface IPluWeightService
{
    public OutputDto<List<PluWeight>> GetAllWeightByArm(Guid uid);
    public OutputDto<WeightLabel> GenerateLabel(Guid armId, Guid pluId, CreateWeightLabelDto dto);
}