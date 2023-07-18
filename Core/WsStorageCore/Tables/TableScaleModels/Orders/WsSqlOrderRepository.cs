namespace WsStorageCore.Tables.TableScaleModels.Orders;

public class WsSqlOrderRepository : WsSqlTableRepositoryBase<WsSqlOrderModel>
{
    public List<WsSqlOrderModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => ContextList.GetListNotNullableOrders(sqlCrudConfig);

}