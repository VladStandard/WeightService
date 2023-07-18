namespace WsStorageCore.Tables.TableDiagModels.LogsTypes;

public class WsSqlLogTypeRepository : WsSqlTableRepositoryBase<WsSqlLogTypeModel>
{
    public List<WsSqlLogTypeModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => 
        ContextList.GetListNotNullableLogsTypes(sqlCrudConfig);
}