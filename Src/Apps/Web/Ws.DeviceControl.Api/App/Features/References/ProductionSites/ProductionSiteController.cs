using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Common;
using Ws.DeviceControl.Models.Features.References.ProductionSites.Commands.Create;
using Ws.DeviceControl.Models.Features.References.ProductionSites.Commands.Update;
using Ws.DeviceControl.Models.Features.References.ProductionSites.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.ProductionSites;

[ApiController]
[Route("api/production-sites")]
public class ProductionSiteController(IProductionSiteService productionSiteService)
{
    #region Queries

    [HttpGet("user-proxy")]
    public Task<ProxyDto> GetProxyByUser() => productionSiteService.GetProxyByUser();

    [HttpGet("proxy")]
    public Task<List<ProxyDto>> GetProxies() => productionSiteService.GetProxiesAsync();

    [HttpGet]
    public Task<List<ProductionSiteDto>> GetAll() => productionSiteService.GetAllAsync();

    [HttpGet("{id:guid}")]
    public Task<ProductionSiteDto> GetById([FromRoute] Guid id) => productionSiteService.GetByIdAsync(id);

    #endregion

    #region Commamnds

    [HttpPost]
    [Authorize(PolicyEnum.Admin)]
    public Task<ProductionSiteDto> Create([FromBody] ProductionSiteCreateDto dto) =>
        productionSiteService.CreateAsync(dto);

    [HttpPost("{id:guid}")]
    [Authorize(PolicyEnum.Admin)]
    public Task<ProductionSiteDto> Update([FromRoute] Guid id, [FromBody] ProductionSiteUpdateDto dto) =>
        productionSiteService.UpdateAsync(id, dto);

    #endregion
}