﻿namespace WsStorageCore.Tables.TableDiagModels.Logs;

public class WsSqlLogRepository : WsSqlTableRepositoryBase<WsSqlLogModel>
{
    public List<WsSqlLogModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.CreateDt), Direction = WsSqlEnumOrder.Desc });
        List<WsSqlLogModel> list = SqlCore.GetListNotNullable<WsSqlLogModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.CreateDt).ToList();
        return list;
    }

    public WsSqlLogModel GetItemByUid(Guid uid) => SqlCore.GetItemNotNullable<WsSqlLogModel>(uid);
    
}