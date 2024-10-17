using Phetch.Core;
using Ws.Mobile.Models;
using Ws.Mobile.Models.Features.Warehouses;

namespace ScalesMobile.Source.Shared.Api.Mobile.Endpoints;

public class WarehousesEndpoints(IMobileApi mobileApi)
{
    public ParameterlessEndpoint<List<WarehouseDto>> WarehousesEndpoint { get; } = new(
        mobileApi.GetWarehouses,
        options: new() { DefaultStaleTime = TimeSpan.FromHours(1) });
}
