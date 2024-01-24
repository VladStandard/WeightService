using Ws.Database.Core.Entities.Scales.TemplatesResources;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Domain.Services.Features.TemplateResource;

internal class TemplateResourceService : ITemplateResourceService
{
    public TemplateResourceEntity GetByUid(Guid uid) => new SqlTemplateResourceRepository().GetByUid(uid);
    public IEnumerable<TemplateResourceEntity> GetAll() => new SqlTemplateResourceRepository().GetList();
}