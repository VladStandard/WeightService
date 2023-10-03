namespace WsStorageCore.Tables.TableScaleModels.Orders;

public class WsSqlOrderRepository : WsSqlTableRepositoryBase<WsSqlOrderModel>
{
    public List<WsSqlOrderModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        return SqlCore.GetEnumerable<WsSqlOrderModel>(sqlCrudConfig).ToList();
    }

}