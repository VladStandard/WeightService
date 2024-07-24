using Ws.DeviceControl.Models.Dto.References.Warehouses.Commands.Create;
using Ws.DeviceControl.Models.Dto.References.Warehouses.Commands.Update;
using Ws.DeviceControl.Models.Dto.References.Warehouses.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.Warehouses.Common;

public interface IWarehouseService
{
    #region Queries

    Task<WarehouseDto> GetByIdAsync(Guid id);
    Task<List<ProxyDto>> GetProxiesByProductionSiteAsync(Guid productionSiteId);
    Task<List<WarehouseDto>> GetAllByProductionSiteAsync(Guid productionSiteId);

    #endregion

    #region Commands

    Task<WarehouseDto> CreateAsync(WarehouseCreateDto dto);
    Task<WarehouseDto> UpdateAsync(Guid id, WarehouseUpdateDto dto);

    #endregion
}