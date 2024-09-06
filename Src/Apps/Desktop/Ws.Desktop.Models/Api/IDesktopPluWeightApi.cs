using Ws.Desktop.Models.Features.Labels.Input;
using Ws.Desktop.Models.Features.Labels.Output;
using Ws.Desktop.Models.Features.Plus.Weight.Output;

namespace Ws.Desktop.Models.Api;

public interface IDesktopPluWeightApi
{
    #region Queries

    [Get("/plu/weight")]
    Task<PluWeight[]> GetPlusWeightByArm();

    #endregion

    #region Commands

    [Post("/plu/weight/{pluUid}/label")]
    Task<WeightLabel> CreatePluWeightLabel(Guid pluUid, [Body] CreateWeightLabelDto createDto);

    #endregion
}