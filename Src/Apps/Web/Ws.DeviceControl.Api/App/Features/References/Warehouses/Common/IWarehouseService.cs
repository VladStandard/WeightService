using Ws.DeviceControl.Models.Dto.References.Warehouses.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.Warehouses.Common;

public interface IWarehouseService
{
    #region Queries

    Task<WarehouseDto> GetByIdAsync(Guid id);
    Task<List<ProxyDto>> GetProxiesByProductionSiteAsync(Guid productionSiteId);
    Task<List<WarehouseDto>> GetAllByProductionSiteAsync(Guid productionSiteId);

    #endregion
}