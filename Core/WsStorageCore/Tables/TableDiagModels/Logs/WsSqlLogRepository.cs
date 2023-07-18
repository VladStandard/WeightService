namespace WsStorageCore.Tables.TableDiagModels.Logs;

public class WsSqlLogRepository : WsSqlTableRepositoryBase<WsSqlLogModel>
{
    public List<WsSqlLogModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => 
        ContextList.GetListNotNullableLogs(sqlCrudConfig);
}