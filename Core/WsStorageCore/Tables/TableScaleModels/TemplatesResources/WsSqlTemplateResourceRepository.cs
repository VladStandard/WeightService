namespace WsStorageCore.Tables.TableScaleModels.TemplatesResources;

public class WsSqlTemplateResourceRepository : WsSqlTableRepositoryBase<WsSqlTemplateResourceModel>
{
    public List<WsSqlTemplateResourceModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<WsSqlTemplateResourceModel> items = SqlCore.GetEnumerable<WsSqlTemplateResourceModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items
                .OrderBy(item => item.Name)
                .ThenBy(item => item.Type);
        return items.ToList();
    }
}