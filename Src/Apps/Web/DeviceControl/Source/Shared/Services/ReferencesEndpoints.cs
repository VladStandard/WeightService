using Phetch.Core;
using Ws.DeviceControl.Models;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Queries;

namespace DeviceControl.Source.Shared.Services;

public class ReferencesEndpoints(IWebApi webApi)
{
    public ParameterlessEndpoint<ProductionSiteDto[]> ProductionSitesEndpoint { get; } = new(
        webApi.GetProductionSites,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });
}