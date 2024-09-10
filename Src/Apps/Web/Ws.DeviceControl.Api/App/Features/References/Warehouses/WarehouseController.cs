using Ws.DeviceControl.Api.App.Features.References.Warehouses.Common;
using Ws.DeviceControl.Models.Features.References.Warehouses.Commands.Create;
using Ws.DeviceControl.Models.Features.References.Warehouses.Commands.Update;
using Ws.DeviceControl.Models.Features.References.Warehouses.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.Warehouses;

[ApiController]
[Route(RouteUtil.Warehouses)]
public class WarehouseController(IWarehouseService warehouseService)
{
    #region Queries

    [Authorize(PolicyEnum.SeniorSupport)]
    [HttpGet]
    public Task<List<WarehouseDto>> GetAllByProductionSite([FromQuery(Name = "productionSite")] Guid productionSiteId) =>
        warehouseService.GetAllByProductionSiteAsync(productionSiteId);

    [Authorize(PolicyEnum.SeniorSupport)]
    [HttpGet("{id:guid}")]
    public Task<WarehouseDto> GetById([FromRoute] Guid id) => warehouseService.GetByIdAsync(id);

    [HttpGet("proxy")]
    public Task<List<ProxyDto>> GetProxiesByProductionSite([FromQuery(Name = "productionSite")] Guid productionSiteId) =>
        warehouseService.GetProxiesByProductionSiteAsync(productionSiteId);

    #endregion

    #region Commands

    [Authorize(PolicyEnum.Admin)]
    [HttpPost]
    public Task<WarehouseDto> Create([FromBody] WarehouseCreateDto dto) =>
        warehouseService.CreateAsync(dto);

    [Authorize(PolicyEnum.Admin)]
    [HttpPut("{id:guid}")]
    public Task<WarehouseDto> Update([FromRoute] Guid id, [FromBody] WarehouseUpdateDto dto) =>
        warehouseService.UpdateAsync(id, dto);

    [Authorize(PolicyEnum.Admin)]
    [HttpDelete("{id:guid}")]
    public Task Delete([FromRoute] Guid id) =>
        warehouseService.DeleteAsync(id);

    #endregion
}