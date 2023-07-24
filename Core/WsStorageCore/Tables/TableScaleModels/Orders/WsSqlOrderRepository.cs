namespace WsStorageCore.Tables.TableScaleModels.Orders;

public class WsSqlOrderRepository : WsSqlTableRepositoryBase<WsSqlOrderModel>
{
    public List<WsSqlOrderModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.ChangeDt), Direction = WsSqlEnumOrder.Desc });
        List<WsSqlOrderModel> list = SqlCore.GetListNotNullable<WsSqlOrderModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.ChangeDt).ToList();
        return list;
    }

}