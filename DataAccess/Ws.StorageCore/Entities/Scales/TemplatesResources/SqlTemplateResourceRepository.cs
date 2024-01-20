using Ws.Domain.Models.Entities.SchemaScale;

namespace Ws.StorageCore.Entities.Scales.TemplatesResources;

public class SqlTemplateResourceRepository : SqlTableRepositoryBase<TemplateResourceEntity>
{
    public IEnumerable<TemplateResourceEntity> GetList()
    {
        SqlCrudConfigModel crud = new();
        IEnumerable<TemplateResourceEntity> items = SqlCore.GetEnumerable<TemplateResourceEntity>(crud);
        return items
            .OrderBy(item => item.Name)
            .ThenBy(item => item.Type);
    }
    
    public TemplateResourceEntity GetByName(string name)
    {
        SqlCrudConfigModel model = new();
        model.AddFilter(SqlRestrictions.Equal(nameof(TemplateResourceEntity.Name), name));
        return SqlCore.GetItemByCrud<TemplateResourceEntity>(model);
   
    }
}