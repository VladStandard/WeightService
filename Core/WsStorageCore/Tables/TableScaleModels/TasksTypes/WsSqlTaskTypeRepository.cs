namespace WsStorageCore.Tables.TableScaleModels.TasksTypes;

public class WsSqlTaskTypeRepository : WsSqlTableRepositoryBase<WsSqlTaskTypeModel>
{
    public List<WsSqlTaskTypeModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerableNotNullable<WsSqlTaskTypeModel>(sqlCrudConfig).ToList();
    }
}