using Ws.DeviceControl.Models.Features.Devices.Arms.Commands.Create;
using Ws.DeviceControl.Models.Features.Devices.Arms.Commands.Update;
using Ws.DeviceControl.Models.Features.Devices.Arms.Queries;

namespace Ws.DeviceControl.Api.App.Features.Devices.Arms.Common;

public interface IArmService : IDeleteService<Guid>
{
    #region Queries

    Task<ArmDto> GetByIdAsync(Guid id);
    Task<List<PluArmDto>> GetArmPlus(Guid id);
    Task<List<ArmDto>> GetAllByProductionSiteAsync(Guid productionSiteId);

    #endregion

    #region Commands

    Task<ArmDto> CreateAsync(ArmCreateDto dto);
    Task<ArmDto> UpdateAsync(Guid id, ArmUpdateDto dto);
    Task AddPluAsync(Guid id, Guid pluId);
    Task DeletePluAsync(Guid armId, Guid pluId);

    #endregion
}