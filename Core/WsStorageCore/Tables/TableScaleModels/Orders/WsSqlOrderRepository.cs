namespace WsStorageCore.Tables.TableScaleModels.Orders;

public class WsSqlOrderRepository : WsSqlTableRepositoryBase<WsSqlOrderModel>
{
    public List<WsSqlOrderModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.ChangeDt), Direction = WsSqlEnumOrder.Desc });
        return SqlCore.GetListNotNullable<WsSqlOrderModel>(sqlCrudConfig);
    }

}