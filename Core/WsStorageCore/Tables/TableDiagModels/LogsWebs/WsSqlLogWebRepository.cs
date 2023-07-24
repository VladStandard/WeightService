﻿namespace WsStorageCore.Tables.TableDiagModels.LogsWebs;

public class WsSqlLogWebRepository: WsSqlTableRepositoryBase<WsSqlLogWebModel>
{
    public List<WsSqlLogWebModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.CreateDt) });
        List<WsSqlLogWebModel> list = SqlCore.GetListNotNullable<WsSqlLogWebModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.CreateDt).ToList();
        return list;
    }
}