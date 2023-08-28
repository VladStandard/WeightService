namespace WsStorageCore.Tables.TableDiagModels.LogsTypes;

public class WsSqlLogTypeRepository : WsSqlTableRepositoryBase<WsSqlLogTypeModel>
{
    
    public WsSqlLogTypeModel GetItemByEnumType(WsEnumLogType logType)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(new() { Name = nameof(WsSqlLogTypeModel.Number), Value = (byte)logType } );
        return SqlCore.GetItemByCrud<WsSqlLogTypeModel>(sqlCrudConfig);
    }
    
    public IEnumerable<WsSqlLogTypeModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(nameof(WsSqlLogTypeModel.Number));
        return SqlCore.GetEnumerableNotNullable<WsSqlLogTypeModel>(sqlCrudConfig);
    }
}