namespace WsStorageCore.Tables.TableScaleModels.OrdersWeighings;

public sealed class WsSqlOrderWeightingRepository : WsSqlTableRepositoryBase<WsSqlOrderWeighingModel>
{
    public List<WsSqlOrderWeighingModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.ChangeDt), Direction = WsSqlEnumOrder.Desc });
        return SqlCore.GetListNotNullable<WsSqlOrderWeighingModel>(sqlCrudConfig);
    }

}