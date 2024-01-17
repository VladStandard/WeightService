namespace Ws.StorageCore.Entities.SchemaScale.TemplatesResources;

public class SqlTemplateResourceRepository : SqlTableRepositoryBase<SqlTemplateResourceEntity>
{
    public IEnumerable<SqlTemplateResourceEntity> GetList()
    {
        SqlCrudConfigModel crud = new();
        IEnumerable<SqlTemplateResourceEntity> items = SqlCore.GetEnumerable<SqlTemplateResourceEntity>(crud);
        return items
            .OrderBy(item => item.Name)
            .ThenBy(item => item.Type);
    }
    
    public SqlTemplateResourceEntity GetByName(string name)
    {
        SqlCrudConfigModel model = new();
        model.AddFilter(SqlRestrictions.Equal(nameof(SqlTemplateResourceEntity.Name), name));
        return SqlCore.GetItemByCrud<SqlTemplateResourceEntity>(model);
   
    }
}