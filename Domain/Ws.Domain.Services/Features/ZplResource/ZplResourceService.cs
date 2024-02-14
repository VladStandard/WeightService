using Ws.Database.Core.Entities.Scales.TemplatesResources;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Domain.Services.Features.ZplResource;

internal class ZplResourceService : IZplResourceService
{
    public TemplateResourceEntity GetItemByUid(Guid uid) => new SqlTemplateResourceRepository().GetByUid(uid);
    public IEnumerable<TemplateResourceEntity> GetAll() => new SqlTemplateResourceRepository().GetAll();
}