using Ws.DeviceControl.Models.Dto.Devices.Arms.Queries;

namespace Ws.DeviceControl.Api.App.Features.Devices.Arms.Common;

public interface IArmService
{
    #region Queries

    Task<List<ArmDto>> GetAllByProductionSiteAsync(Guid productionSiteId);
    Task<ArmDto> GetByIdAsync(Guid id);

    #endregion
}