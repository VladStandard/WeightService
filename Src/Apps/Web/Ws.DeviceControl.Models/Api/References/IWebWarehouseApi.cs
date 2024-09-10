using Ws.DeviceControl.Models.Features.References.Warehouses.Commands.Create;
using Ws.DeviceControl.Models.Features.References.Warehouses.Commands.Update;
using Ws.DeviceControl.Models.Features.References.Warehouses.Queries;

namespace Ws.DeviceControl.Models.Api.References;

public interface IWebWarehouseApi
{
    #region Queries

    [Get("/warehouses/{uid}")]
    Task<WarehouseDto> GetWarehouseByUid(Guid uid);

    [Get("/warehouses?productionSite={productionSiteUid}")]
    Task<WarehouseDto[]> GetWarehousesByProductionSite(Guid productionSiteUid);

    [Get("/warehouses/proxy?productionSite={productionSiteUid}")]
    Task<ProxyDto[]> GetProxyWarehousesByProductionSite(Guid productionSiteUid);

    #endregion

    #region Commands

    [Delete("/warehouses/{uid}")]
    Task DeleteWarehouse(Guid uid);

    [Post("/warehouses")]
    Task<WarehouseDto> CreateWarehouse([Body] WarehouseCreateDto createDto);

    [Post("/warehouses/{uid}")]
    Task<WarehouseDto> UpdateWarehouse(Guid uid, [Body] WarehouseUpdateDto updateDto);

    #endregion
}