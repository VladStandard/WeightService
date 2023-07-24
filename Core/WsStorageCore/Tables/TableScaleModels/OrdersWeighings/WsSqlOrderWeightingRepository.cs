namespace WsStorageCore.Tables.TableScaleModels.OrdersWeighings;

public sealed class WsSqlOrderWeightingRepository : WsSqlTableRepositoryBase<WsSqlOrderWeighingModel>
{
    public List<WsSqlOrderWeighingModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.ChangeDt), Direction = WsSqlEnumOrder.Desc });
        List<WsSqlOrderWeighingModel> list = SqlCore.GetListNotNullable<WsSqlOrderWeighingModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.ChangeDt).ToList();
        return list;
    }

}