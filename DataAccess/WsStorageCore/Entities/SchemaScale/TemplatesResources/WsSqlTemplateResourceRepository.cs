namespace WsStorageCore.Entities.SchemaScale.TemplatesResources;

public class WsSqlTemplateResourceRepository : WsSqlTableRepositoryBase<WsSqlTemplateResourceEntity>
{
    public List<WsSqlTemplateResourceEntity> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<WsSqlTemplateResourceEntity> items = SqlCore.GetEnumerable<WsSqlTemplateResourceEntity>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items
                .OrderBy(item => item.Name)
                .ThenBy(item => item.Type);
        return items.ToList();
    }
}