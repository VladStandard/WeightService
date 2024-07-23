using Phetch.Core;
using Ws.DeviceControl.Models;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Queries;
using Ws.DeviceControl.Models.Dto.References.Warehouses.Queries;
using Ws.DeviceControl.Models.Dto.Shared;

namespace DeviceControl.Source.Shared.Services;

public class ReferencesEndpoints(IWebApi webApi)
{
    public ParameterlessEndpoint<ProductionSiteDto[]> ProductionSitesEndpoint { get; } = new(
        webApi.GetProductionSites,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public Endpoint<Guid, WarehouseDto[]> WarehousesEndpoint { get; } = new(
        webApi.GetWarehousesByProductionSite,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public Endpoint<Guid, ProxyDto[]> ProxyWarehousesEndpoint { get; } = new(
        webApi.GetProxyWarehousesByProductionSite,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });
}