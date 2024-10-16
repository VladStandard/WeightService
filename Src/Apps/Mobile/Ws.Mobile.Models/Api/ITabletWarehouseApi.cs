using Ws.Mobile.Models.Features.Warehouses;

namespace Ws.Mobile.Models.Api;

public interface ITabletWarehouseApi
{
    [Get("/warehouses")]
    Task<List<WarehouseDto>> GetWarehouses();
}