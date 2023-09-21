namespace WsStorageCore.Tables.TableDiagModels.LogsTypes;

public class WsSqlLogTypeRepository : WsSqlTableRepositoryBase<WsSqlLogTypeModel>
{
    
    public WsSqlLogTypeModel GetItemByEnumType(WsEnumLogType logType)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(
            SqlRestrictions.Equal(nameof(WsSqlLogTypeModel.Number), (byte)logType)
        );
        return SqlCore.GetItemByCrud<WsSqlLogTypeModel>(sqlCrudConfig);
    }
    
    public IEnumerable<WsSqlLogTypeModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.Asc(nameof(WsSqlLogTypeModel.Number)));
        return SqlCore.GetEnumerableNotNullable<WsSqlLogTypeModel>(sqlCrudConfig);
    }
}