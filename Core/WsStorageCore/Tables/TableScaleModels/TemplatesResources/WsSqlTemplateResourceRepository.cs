namespace WsStorageCore.Tables.TableScaleModels.TemplatesResources;

public class WsSqlTemplateResourceRepository : WsSqlTableRepositoryBase<WsSqlTemplateResourceModel>
{
    public List<WsSqlTemplateResourceModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => ContextList.GetListNotNullableTemplateResources(sqlCrudConfig);
}