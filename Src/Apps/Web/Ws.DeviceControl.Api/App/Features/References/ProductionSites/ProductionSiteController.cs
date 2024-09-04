using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Common;
using Ws.DeviceControl.Models.Features.References.ProductionSites.Commands.Create;
using Ws.DeviceControl.Models.Features.References.ProductionSites.Commands.Update;
using Ws.DeviceControl.Models.Features.References.ProductionSites.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.ProductionSites;

[ApiController]
[Route(RouteUtil.ProductionSites)]
public class ProductionSiteController(IProductionSiteService productionSiteService)
{
    #region Queries

    #region Admin

    [Authorize(PolicyEnum.SeniorSupport)]
    [HttpGet]
    public Task<List<ProductionSiteDto>> GetAll() => productionSiteService.GetAllAsync();

    [Authorize(PolicyEnum.SeniorSupport)]
    [HttpGet("{id:guid}")]
    public Task<ProductionSiteDto> GetById([FromRoute] Guid id) => productionSiteService.GetByIdAsync(id);

    #endregion

    [HttpGet("user-proxy")]
    public Task<ProxyDto> GetProxyByUser() => productionSiteService.GetProxyByUser();

    [HttpGet("proxy")]
    public Task<List<ProxyDto>> GetProxies() => productionSiteService.GetProxiesAsync();

    #endregion

    #region Commamnds

    [Authorize(PolicyEnum.Admin)]
    [HttpPost]
    public Task<ProductionSiteDto> Create([FromBody] ProductionSiteCreateDto dto) =>
        productionSiteService.CreateAsync(dto);

    [Authorize(PolicyEnum.Admin)]
    [HttpPost("{id:guid}")]
    public Task<ProductionSiteDto> Update([FromRoute] Guid id, [FromBody] ProductionSiteUpdateDto dto) =>
        productionSiteService.UpdateAsync(id, dto);

    [Authorize(PolicyEnum.Admin)]
    [HttpPost("{id:guid}/delete")]
    public Task Delete([FromRoute] Guid id) =>
        productionSiteService.DeleteAsync(id);

    #endregion
}