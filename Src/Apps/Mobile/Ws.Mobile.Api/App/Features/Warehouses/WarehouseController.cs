using Ws.Mobile.Api.App.Features.Warehouses.Common;
using Ws.Mobile.Models.Features.Warehouses;

namespace Ws.Mobile.Api.App.Features.Warehouses;

[ApiController]
[Route(ApiEndpoints.Warehouses)]
public sealed class WarehouseController(IWarehouseService warehouseService)
{
    #region Queries

    [HttpGet]
    public List<WarehouseDto> GetAll() => warehouseService.GetAll();

    #endregion
}