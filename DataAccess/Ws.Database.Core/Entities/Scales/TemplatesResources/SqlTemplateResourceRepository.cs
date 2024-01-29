using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Core.Entities.Scales.TemplatesResources;

public class SqlTemplateResourceRepository : IGetItemByUid<TemplateResourceEntity>, IGetAll<TemplateResourceEntity>
{
    public TemplateResourceEntity GetByUid(Guid uid) =>
        SqlCoreHelper.Instance.GetItemById<TemplateResourceEntity>(uid);
    
    public IEnumerable<TemplateResourceEntity> GetAll()
    {
        DetachedCriteria criteria = DetachedCriteria.For<TemplateResourceEntity>()
            .AddOrder(SqlOrder.NameAsc());
        return SqlCoreHelper.Instance.GetEnumerable<TemplateResourceEntity>(criteria);
    }
    
    public TemplateResourceEntity GetByName(string name)
    {
        DetachedCriteria criteria = DetachedCriteria.For<TemplateResourceEntity>()
            .Add(SqlRestrictions.Equal(nameof(TemplateResourceEntity.Name), name));
        return SqlCoreHelper.Instance.GetItemByCriteria<TemplateResourceEntity>(criteria);
   
    }
}