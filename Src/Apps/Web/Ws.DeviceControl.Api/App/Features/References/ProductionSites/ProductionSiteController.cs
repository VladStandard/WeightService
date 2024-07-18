using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Common;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Commands.Create;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Commands.Update;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.ProductionSites;

[ApiController]
[Route("api/production-sites/")]
public class ProductionSiteController(IProductionSiteService productionSiteService)
{
    #region Queries

    [HttpGet]
    public Task<List<ProductionSiteDto>> GetAll() => productionSiteService.GetAllAsync();

    [HttpGet("{id:guid}")]
    public Task<ProductionSiteDto> GetById([FromRoute] Guid id) => productionSiteService.GetByIdAsync(id);

    #endregion

    #region Commamnds

    [HttpPost]
    public Task<ProductionSiteDto> Create([FromBody] ProductionSiteCreateDto dto) =>
        productionSiteService.CreateAsync(dto);

    [HttpPut("{id:guid}")]
    public Task<ProductionSiteDto> Update([FromRoute] Guid id, [FromBody] ProductionSiteUpdateDto dto) =>
        productionSiteService.UpdateAsync(id, dto);

    #endregion
}