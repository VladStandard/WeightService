using Phetch.Core;
using Ws.DeviceControl.Models;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Queries;
using Ws.DeviceControl.Models.Dto.References.Warehouses.Queries;

namespace DeviceControl.Source.Shared.Services;

public class ReferencesEndpoints(IWebApi webApi)
{
    public ParameterlessEndpoint<ProductionSiteDto[]> ProductionSitesEndpoint { get; } = new(
        webApi.GetProductionSites,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public Endpoint<Guid, WarehouseDto[]> WarehousesEndpoint { get; } = new(
        webApi.GetWarehouses,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });
}