using Ws.DeviceControl.Models.Dto.References.TemplateResources.Commands.Create;
using Ws.DeviceControl.Models.Dto.References.TemplateResources.Commands.Update;
using Ws.DeviceControl.Models.Dto.References.TemplateResources.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.TemplateResources.Common;

public interface ITemplateResourceService : IGetApiService<TemplateResourceDto>
{
    #region Queries

    public Task<TemplateResourceBodyDto> GetBodyByIdAsync(Guid id);

    #endregion

    #region Commmands

    Task<TemplateResourceDto> UpdateAsync(Guid id, TemplateResourceUpdateDto dto);
    Task<TemplateResourceDto> CreateAsync(TemplateResourceCreateDto dto);

    #endregion
}