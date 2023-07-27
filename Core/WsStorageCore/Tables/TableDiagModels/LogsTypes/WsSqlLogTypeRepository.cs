// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableDiagModels.LogsTypes;

public class WsSqlLogTypeRepository : WsSqlTableRepositoryBase<WsSqlLogTypeModel>
{
    
    public WsSqlLogTypeModel GetItemByEnumType(WsEnumLogType logType)
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new() { new() { Name = nameof(WsSqlLogTypeModel.Number), Value = (byte)logType } },
            WsSqlEnumIsMarked.ShowAll, true, false, false);
        return SqlCore.GetItemByCrud<WsSqlLogTypeModel>(sqlCrudConfig);
    }
    
    public List<WsSqlLogTypeModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlLogTypeModel.Number) });
        return SqlCore.GetListNotNullable<WsSqlLogTypeModel>(sqlCrudConfig);
    }
}