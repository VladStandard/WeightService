using Ws.Domain.Models.Entities.SchemaScale;
using Ws.StorageCore.Entities.Scales.TemplatesResources;
using Ws.StorageCore.Helpers;

namespace Ws.Services.Features.TemplateResource;

internal class TemplateResourceService : ITemplateResourceService
{
    public TemplateResourceEntity GetByUid(Guid uid) =>
        SqlCoreHelper.Instance.GetItemByUid<TemplateResourceEntity>(uid);

    public IEnumerable<TemplateResourceEntity> GetAll() => new SqlTemplateResourceRepository().GetList();
}