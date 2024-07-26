using Ws.DeviceControl.Api.App.Features.References.Templates.Common;
using Ws.DeviceControl.Models.Dto.References.Template.Commands.Create;
using Ws.DeviceControl.Models.Dto.References.Template.Commands.Update;
using Ws.DeviceControl.Models.Dto.References.Template.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.Templates;

[ApiController]
[Route("api/templates")]
public class TemplateController(ITemplateService templateService)
{
    #region Queries

    [HttpGet("proxy")]
    public Task<List<ProxyDto>> GetProxiesByPlu([FromQuery(Name = "plu")] Guid pluId)
        => templateService.GetProxiesByPluAsync(pluId);

    [HttpGet]
    public Task<List<TemplateDto>> GetAll() => templateService.GetAllAsync();

    [HttpGet("{id:guid}")]
    public Task<TemplateDto> GetById([FromRoute] Guid id) => templateService.GetByIdAsync(id);

    #endregion

    #region Commands

    [HttpPost]
    public Task<TemplateDto> Create([FromBody] TemplateCreateDto dto) =>
        templateService.CreateAsync(dto);

    [HttpPost("{id:guid}")]
    public Task<TemplateDto> Update([FromRoute] Guid id, [FromBody] TemplateUpdateDto dto) =>
        templateService.UpdateAsync(id, dto);

    #endregion
}