using Ws.DeviceControl.Api.App.Features.References.Warehouses.Common;
using Ws.DeviceControl.Models.Dto.References.Warehouses.Commands.Create;
using Ws.DeviceControl.Models.Dto.References.Warehouses.Commands.Update;
using Ws.DeviceControl.Models.Dto.References.Warehouses.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.Warehouses;

[ApiController]
[Route("api/warehouses")]
public class WarehouseController(IWarehouseService warehouseService)
{
    #region Queries

    [HttpGet("proxy")]
    public Task<List<ProxyDto>> GetProxiesByProductionSite([FromQuery(Name = "productionSite")] Guid productionSiteId) =>
        warehouseService.GetProxiesByProductionSiteAsync(productionSiteId);

    [HttpGet]
    public Task<List<WarehouseDto>> GetAllByProductionSite([FromQuery(Name = "productionSite")] Guid productionSiteId) =>
        warehouseService.GetAllByProductionSiteAsync(productionSiteId);

    [HttpGet("{id:guid}")]
    public Task<WarehouseDto> GetById([FromRoute] Guid id) => warehouseService.GetByIdAsync(id);

    #endregion

    #region Commands

    [HttpPost]
    public Task<WarehouseDto> Create([FromBody] WarehouseCreateDto dto) =>
        warehouseService.CreateAsync(dto);

    [HttpPost("{id:guid}")]
    public Task<WarehouseDto> Update([FromRoute] Guid id, [FromBody] WarehouseUpdateDto dto) =>
        warehouseService.UpdateAsync(id, dto);

    #endregion
}