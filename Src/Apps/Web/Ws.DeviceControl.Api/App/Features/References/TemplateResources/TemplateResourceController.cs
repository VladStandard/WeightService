using Ws.DeviceControl.Api.App.Features.References.TemplateResources.Common;
using Ws.DeviceControl.Models.Features.References.TemplateResources.Commands;
using Ws.DeviceControl.Models.Features.References.TemplateResources.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.TemplateResources;

[ApiController]
[Route(ApiEndpoints.TemplateResources)]
public sealed class TemplateResourceController(ITemplateResourceService templateResourceService)
{
    #region Queries

    [Authorize(PolicyEnum.SeniorSupport)]
    [HttpGet]
    public Task<List<TemplateResourceDto>> GetAll() => templateResourceService.GetAllAsync();

    [Authorize(PolicyEnum.SeniorSupport)]
    [HttpGet("{id:guid}")]
    public Task<TemplateResourceDto> GetById([FromRoute] Guid id) => templateResourceService.GetByIdAsync(id);

    [Authorize(PolicyEnum.SeniorSupport)]
    [HttpGet("{id:guid}/body")]
    public Task<TemplateResourceBodyDto> GetBodyById([FromRoute] Guid id) => templateResourceService.GetBodyByIdAsync(id);

    #endregion

    #region Commands

    [Authorize(PolicyEnum.Admin)]
    [HttpPost]
    public Task<TemplateResourceDto> Create([FromBody] TemplateResourceCreateDto dto) =>
        templateResourceService.CreateAsync(dto);

    [Authorize(PolicyEnum.Admin)]
    [HttpPut("{id:guid}")]
    public Task<TemplateResourceDto> Update([FromRoute] Guid id, [FromBody] TemplateResourceUpdateDto dto) =>
        templateResourceService.UpdateAsync(id, dto);

    [Authorize(PolicyEnum.Admin)]
    [HttpDelete("{id:guid}")]
    public Task Delete([FromRoute] Guid id) => templateResourceService.DeleteAsync(id);

    #endregion
}