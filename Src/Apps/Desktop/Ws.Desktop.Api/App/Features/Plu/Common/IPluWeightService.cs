using Ws.Desktop.Models.Features.Labels.Input;
using Ws.Desktop.Models.Features.Labels.Output;
using Ws.Desktop.Models.Features.Plus.Weight.Output;

namespace Ws.Desktop.Api.App.Features.Plu.Common;

public interface IPluWeightService
{
    #region Queries

    public List<PluWeight> GetAllWeightByArm(Guid uid);

    #endregion

    #region Commands

    public WeightLabel GenerateLabel(Guid armId, Guid pluId, CreateWeightLabelDto dto);

    #endregion
}