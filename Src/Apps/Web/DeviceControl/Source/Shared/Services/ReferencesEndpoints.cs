using Phetch.Core;
using Ws.DeviceControl.Models;
using Ws.DeviceControl.Models.Dto.Devices.Arms.Queries;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Queries;
using Ws.DeviceControl.Models.Dto.References.Warehouses.Queries;
using Ws.DeviceControl.Models.Dto.Shared;
using Ws.Shared.Extensions;

namespace DeviceControl.Source.Shared.Services;

public class ReferencesEndpoints(IWebApi webApi)
{
    # region Production Site

    public ParameterlessEndpoint<ProductionSiteDto[]> ProductionSitesEndpoint { get; } = new(
        webApi.GetProductionSites,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddProductionSite(ProductionSiteDto productionSite) =>
        ProductionSitesEndpoint.UpdateQueryData(new(), query =>
            query.Data == null ? query.Data! : query.Data.Prepend(productionSite).ToArray());

    public void UpdateProductionSite(ProductionSiteDto productionSite) =>
        ProductionSitesEndpoint.UpdateQueryData(new(), query =>
            query.Data == null ? query.Data! : query.Data.ReplaceItemByKey(productionSite, p => p.Id == productionSite.Id).ToArray());

    public void DeleteProductionSite(Guid productionSiteId) =>
        ProductionSitesEndpoint.UpdateQueryData(new(), query =>
            query.Data == null ? query.Data! : query.Data.Where(x => x.Id != productionSiteId).ToArray());

    # endregion

    # region Warehouse

    public Endpoint<Guid, WarehouseDto[]> WarehousesEndpoint { get; } = new(
        webApi.GetWarehousesByProductionSite,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddWarehouse(Guid productionSiteId, WarehouseDto warehouse)
    {
        WarehousesEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.Prepend(warehouse).ToArray());
        AddProxyWarehouse(productionSiteId, new() { Id = warehouse.Id, Name = warehouse.Name });
    }

    public void UpdateWarehouse(Guid productionSiteId, WarehouseDto warehouse)
    {
        WarehousesEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.ReplaceItemByKey(warehouse, p => p.Id == warehouse.Id).ToArray());
        UpdateProxyWarehouse(productionSiteId, new() { Id = warehouse.Id, Name = warehouse.Name });
    }

    public void DeleteWarehouse(Guid productionSiteId, Guid warehouseId)
    {
        WarehousesEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.Where(x => x.Id != warehouseId).ToArray());
        DeleteProxyWarehouse(productionSiteId, warehouseId);
    }

    # endregion

    # region Proxt Warehouse

    public Endpoint<Guid, ProxyDto[]> ProxyWarehousesEndpoint { get; } = new(
        webApi.GetProxyWarehousesByProductionSite,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddProxyWarehouse(Guid productionSiteId, ProxyDto warehouse) =>
        ProxyWarehousesEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.Prepend(warehouse).ToArray());

    public void UpdateProxyWarehouse(Guid productionSiteId, ProxyDto warehouse) =>
        ProxyWarehousesEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.ReplaceItemByKey(warehouse, p => p.Id == warehouse.Id).ToArray());

    public void DeleteProxyWarehouse(Guid productionSiteId, Guid warehouseId) =>
        ProxyWarehousesEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.Where(x => x.Id != warehouseId).ToArray());

    # endregion
}