using Ws.DeviceControl.Models.Features.References.Warehouses.Commands;
using Ws.DeviceControl.Models.Features.References.Warehouses.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.Warehouses.Common;

public interface IWarehouseService : IDeleteService<Guid>
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