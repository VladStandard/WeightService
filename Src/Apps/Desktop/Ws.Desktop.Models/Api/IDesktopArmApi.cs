using Ws.Desktop.Models.Features.Arms.Input;
using Ws.Desktop.Models.Features.Arms.Output;

namespace Ws.Desktop.Models.Api;

public interface IDesktopArmApi
{
    #region Queries

    [Get("/arms")]
    Task<ArmValue> GetArmByName();

    #endregion

    #region Commands

    [Put("/arms")]
    Task UpdateArm([Body] UpdateArmDto updateDto);

    #endregion
}