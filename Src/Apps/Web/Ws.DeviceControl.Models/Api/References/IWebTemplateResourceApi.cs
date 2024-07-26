using Ws.DeviceControl.Models.Dto.References.TemplateResources.Commands.Create;
using Ws.DeviceControl.Models.Dto.References.TemplateResources.Commands.Update;
using Ws.DeviceControl.Models.Dto.References.TemplateResources.Queries;

namespace Ws.DeviceControl.Models.Api.References;

public interface IWebTemplateResourceApi
{
    #region Queries

    [Get("/template-resources")]
    Task<TemplateResourceDto[]> GetResources();

    [Get("/template-resources/{id}")]
    Task<TemplateResourceDto> GetResourceByUid(Guid id);

    #endregion

    #region Commands

    [Delete("/template-resources/{id}")]
    Task<bool> DeleteResource(Guid id);

    [Post("/template-resources")]
    Task<TemplateResourceDto> CreateResource([Body] TemplateResourceCreateDto createDto);

    [Post("/template-resources/{id}")]
    Task<TemplateResourceDto> UpdateResource(Guid id, [Body] TemplateResourceUpdateDto updateDto);

    #endregion
}