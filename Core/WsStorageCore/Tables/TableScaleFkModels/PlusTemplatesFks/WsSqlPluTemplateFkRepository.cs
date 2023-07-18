namespace WsStorageCore.Tables.TableScaleFkModels.PlusTemplatesFks;

public class WsSqlPluTemplateFkRepository : WsSqlTableRepositoryBase<WsSqlPluStorageMethodFkModel>
{
    public List<WsSqlPluTemplateFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => ContextList.GetListNotNullablePlusTemplatesFks(sqlCrudConfig);

}