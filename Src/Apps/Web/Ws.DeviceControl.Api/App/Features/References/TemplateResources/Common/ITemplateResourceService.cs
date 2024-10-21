using Ws.DeviceControl.Models.Features.References.TemplateResources.Commands;
using Ws.DeviceControl.Models.Features.References.TemplateResources.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.TemplateResources.Common;

public interface ITemplateResourceService : IGetApiService<TemplateResourceDto>, IDeleteService<Guid>
{
    #region Queries

    public Task<TemplateResourceBodyDto> GetBodyByIdAsync(Guid id);

    #endregion

    #region Commmands

    Task<TemplateResourceDto> CreateAsync(TemplateResourceCreateDto dto);
    Task<TemplateResourceDto> UpdateAsync(Guid id, TemplateResourceUpdateDto dto);

    #endregion
}