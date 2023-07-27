namespace WsStorageCore.Tables.TableScaleModels.Orders;

public class WsSqlOrderRepository : WsSqlTableRepositoryBase<WsSqlOrderModel>
{
    public List<WsSqlOrderModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(new(nameof(WsSqlTableBase.ChangeDt), WsSqlEnumOrder.Desc));
        return SqlCore.GetListNotNullable<WsSqlOrderModel>(sqlCrudConfig);
    }

}