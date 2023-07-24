namespace WsStorageCore.Tables.TableScaleModels.TemplatesResources;

public class WsSqlTemplateResourceRepository : WsSqlTableRepositoryBase<WsSqlTemplateResourceModel>
{
    public List<WsSqlTemplateResourceModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlTemplateResourceModel> list = SqlCore.GetListNotNullable<WsSqlTemplateResourceModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list
                .OrderBy(item => item.Name)
                .ThenBy(item => item.Type).ToList();
        return list;
    }
}