using Ws.Domain.Models.Entities.SchemaScale;

namespace Ws.Database.Core.Entities.Scales.TemplatesResources;

public class SqlTemplateResourceRepository : IUidRepo<TemplateResourceEntity>
{
    public TemplateResourceEntity GetByUid(Guid uid) =>
        SqlCoreHelper.Instance.GetItemByUid<TemplateResourceEntity>(uid);
    
    public IEnumerable<TemplateResourceEntity> GetList()
    {
        SqlCrudConfigModel crud = new();
        IEnumerable<TemplateResourceEntity> items = SqlCoreHelper.Instance.GetEnumerable<TemplateResourceEntity>(crud);
        return items
            .OrderBy(item => item.Name)
            .ThenBy(item => item.Type);
    }
    
    public TemplateResourceEntity GetByName(string name)
    {
        SqlCrudConfigModel model = new();
        model.AddFilter(SqlRestrictions.Equal(nameof(TemplateResourceEntity.Name), name));
        return SqlCoreHelper.Instance.GetItemByCrud<TemplateResourceEntity>(model);
   
    }
}