namespace WsStorageCore.Tables.TableDiagModels.LogsWebs;

public class WsSqlLogWebRepository: WsSqlTableRepositoryBase<WsSqlLogWebModel>
{
    public List<WsSqlLogWebModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => 
        ContextList.GetListNotNullableLogsWebs(sqlCrudConfig);
}