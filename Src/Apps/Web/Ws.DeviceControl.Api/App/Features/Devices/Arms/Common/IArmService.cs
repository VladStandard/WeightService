using Ws.DeviceControl.Models.Dto.Devices.Arms.Commands.Create;
using Ws.DeviceControl.Models.Dto.Devices.Arms.Commands.Update;
using Ws.DeviceControl.Models.Dto.Devices.Arms.Queries;

namespace Ws.DeviceControl.Api.App.Features.Devices.Arms.Common;

public interface IArmService
{
    #region Queries

    Task<ArmDto> GetByIdAsync(Guid id);
    Task<List<ArmDto>> GetAllByProductionSiteAsync(Guid productionSiteId);

    #endregion


    #region Commands

    Task<ArmDto> CreateAsync(ArmCreateDto dto);
    Task<ArmDto> UpdateAsync(Guid id, ArmUpdateDto dto);

    #endregion
}