using Ws.DeviceControl.Models.Features.Devices.Arms.Commands.Create;
using Ws.DeviceControl.Models.Features.Devices.Arms.Commands.Update;
using Ws.DeviceControl.Models.Features.Devices.Arms.Queries;

namespace Ws.DeviceControl.Models.Api.Devices;

public interface IWebArmApi
{
    #region Queries

    [Get("/arms/{uid}")]
    Task<ArmDto> GetArmByUid(Guid uid);

    [Get("/arms?productionSite={productionSiteUid}")]
    Task<ArmDto[]> GetArmsByProductionSite(Guid productionSiteUid);

    [Get("/arms/{uid}/plus")]
    Task<PluArmDto[]> GetArmPlus(Guid uid);

    #endregion

    #region Commands

    [Delete("/arms/{uid}")]
    Task<bool> DeleteArm(Guid uid);

    [Post("/arms")]
    Task<ArmDto> CreateArm([Body] ArmCreateDto createDto);

    [Post("/arms/{uid}")]
    Task<ArmDto> UpdateArm(Guid uid, [Body] ArmUpdateDto updateDto);

    [Post("/arms/{uid}/plus/{pluId}")]
    Task<bool> DeleteArmPlu(Guid uid, Guid pluId);

    #endregion
}