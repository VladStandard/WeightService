using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Core.Entities.Scales.TemplatesResources;

public class SqlTemplateResourceRepository : IGetItemByUid<TemplateResourceEntity>, IGetAll<TemplateResourceEntity>
{
    public TemplateResourceEntity GetByUid(Guid uid) =>
        SqlCoreHelper.Instance.GetItemById<TemplateResourceEntity>(uid);
    
    public IEnumerable<TemplateResourceEntity> GetAll()
    {
        return SqlCoreHelper.Instance.GetEnumerable<TemplateResourceEntity>(
            DetachedCriteria.For<TemplateResourceEntity>().AddOrder(SqlOrder.NameAsc())
        );
    }
    
    public TemplateResourceEntity GetByName(string name)
    {
        return SqlCoreHelper.Instance.GetItem<TemplateResourceEntity>(
            DetachedCriteria.For<TemplateResourceEntity>()
                .Add(SqlRestrictions.Equal(nameof(TemplateResourceEntity.Name), name))
        );
    }
}