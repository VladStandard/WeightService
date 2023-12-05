namespace Ws.StorageCore.Entities.SchemaScale.TemplatesResources;

public class SqlTemplateResourceRepository : SqlTableRepositoryBase<SqlTemplateResourceEntity>
{
    public List<SqlTemplateResourceEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<SqlTemplateResourceEntity> items = SqlCore.GetEnumerable<SqlTemplateResourceEntity>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items
                .OrderBy(item => item.Name)
                .ThenBy(item => item.Type);
        return items.ToList();
    }
    
    public SqlTemplateResourceEntity GetByName(string name)
    {
        SqlCrudConfigModel model = new();
        model.AddFilter(SqlRestrictions.Equal(nameof(SqlTemplateResourceEntity.Name), name));
        return SqlCore.GetItemByCrud<SqlTemplateResourceEntity>(model);
   
    }
}