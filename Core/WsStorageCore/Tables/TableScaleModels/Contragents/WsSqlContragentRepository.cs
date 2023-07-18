namespace WsStorageCore.Tables.TableScaleModels.Contragents;

public class WsSqlContragentRepository : WsSqlTableRepositoryBase<WsSqlContragentModel>
{
    public List<WsSqlContragentModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => ContextList.GetListNotNullableContragents(sqlCrudConfig);
}