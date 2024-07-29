using Ws.DeviceControl.Api.App.Features.References.TemplateResources.Common;
using Ws.DeviceControl.Models.Dto.References.TemplateResources.Commands.Create;
using Ws.DeviceControl.Models.Dto.References.TemplateResources.Commands.Update;
using Ws.DeviceControl.Models.Dto.References.TemplateResources.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.TemplateResources;

[ApiController]
[Route("api/template-resources")]
public class TemplateResourceController(ITemplateResourceService templateResourceService)
{
    #region Queries

    [HttpGet]
    public Task<List<TemplateResourceDto>> GetAll() => templateResourceService.GetAllAsync();

    [HttpGet("{id:guid}")]
    public Task<TemplateResourceDto> GetById([FromRoute] Guid id) => templateResourceService.GetByIdAsync(id);

    [HttpGet("{id:guid}/body")]
    public Task<TemplateResourceBodyDto> GetBodyById([FromRoute] Guid id) => templateResourceService.GetBodyByIdAsync(id);

    #endregion

    #region Commands

    [HttpPost]
    public Task<TemplateResourceDto> Create([FromBody] TemplateResourceCreateDto dto) =>
        templateResourceService.CreateAsync(dto);

    [HttpPost("{id:guid}")]
    public Task<TemplateResourceDto> Update([FromRoute] Guid id, [FromBody] TemplateResourceUpdateDto dto) =>
        templateResourceService.UpdateAsync(id, dto);

    #endregion
}