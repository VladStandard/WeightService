using Ws.Desktop.Models.Features.Labels.Input;
using Ws.Desktop.Models.Features.Labels.Output;
using Ws.Desktop.Models.Features.Plus.Weight.Output;

namespace Ws.Desktop.Models.Api;

public interface IDesktopPluWeightApi
{
    #region Queries

    [Get("/arms/{armUid}/plu/weight")]
    Task<PluWeight[]> GetPlusWeightByArm(Guid armUid);

    #endregion

    #region Commands

    [Post("/arms/{armUid}/plu/weight/{pluUid}/label")]
    Task<WeightLabel> CreatePluWeightLabel(Guid armUid, Guid pluUid, [Body] CreateWeightLabelDto createDto);

    #endregion
}