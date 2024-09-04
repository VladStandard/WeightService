using Ws.DeviceControl.Api.App.Features.References.Templates.Common;
using Ws.DeviceControl.Models.Features.References.Template.Commands.Create;
using Ws.DeviceControl.Models.Features.References.Template.Commands.Update;
using Ws.DeviceControl.Models.Features.References.Template.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.Templates;

[ApiController]
[Route(RouteUtil.Templates)]
public class TemplateController(ITemplateService templateService)
{
    #region Queries

    [Authorize(PolicyEnum.SeniorSupport)]
    [HttpGet]
    public Task<List<TemplateDto>> GetAll() => templateService.GetAllAsync();

    [Authorize(PolicyEnum.SeniorSupport)]
    [HttpGet("{id:guid}")]
    public Task<TemplateDto> GetById([FromRoute] Guid id) => templateService.GetByIdAsync(id);

    [Authorize(PolicyEnum.SeniorSupport)]
    [HttpGet("{id:guid}/body")]
    public Task<TemplateBodyDto> GetBodyById([FromRoute] Guid id) => templateService.GetBodyByIdAsync(id);

    [HttpGet("proxy")]
    public Task<List<ProxyDto>> GetProxiesByIsWeight([FromQuery(Name = "isWeight")] bool isWeight) =>
        templateService.GetProxiesByIsWeightAsync(isWeight);

    #endregion

    #region Commands

    [Authorize(PolicyEnum.Admin)]
    [HttpPost]
    public Task<TemplateDto> Create([FromBody] TemplateCreateDto dto) =>
        templateService.CreateAsync(dto);

    [Authorize(PolicyEnum.Admin)]
    [HttpPost("{id:guid}")]
    public Task<TemplateDto> Update([FromRoute] Guid id, [FromBody] TemplateUpdateDto dto) =>
        templateService.UpdateAsync(id, dto);

    [Authorize(PolicyEnum.Admin)]
    [HttpPost("{id:guid}/delete")]
    public Task Delete([FromRoute] Guid id) =>
        templateService.DeleteAsync(id);

    #endregion
}