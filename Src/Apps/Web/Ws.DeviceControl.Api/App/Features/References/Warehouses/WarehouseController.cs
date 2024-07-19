using Ws.DeviceControl.Api.App.Features.References.Warehouses.Common;
using Ws.DeviceControl.Models.Dto.References.Warehouses.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.Warehouses;

[ApiController]
[Route("api/warehouses/")]
public class WarehouseController(IWarehouseService printerService)
{
    #region Queries

    [HttpGet("proxy")]
    public Task<List<ProxyDto>> GetProxiesByProductionSite([FromQuery(Name = "productionSite")] Guid productionSiteId) =>
        printerService.GetProxiesByProductionSiteAsync(productionSiteId);

    [HttpGet]
    public Task<List<WarehouseDto>> GetAllByProductionSite([FromQuery(Name = "productionSite")] Guid productionSiteId) =>
        printerService.GetAllByProductionSiteAsync(productionSiteId);

    [HttpGet("{id:guid}")]
    public Task<WarehouseDto> GetById([FromRoute] Guid id) => printerService.GetByIdAsync(id);

    #endregion
}