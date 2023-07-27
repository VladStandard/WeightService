namespace WsStorageCore.Tables.TableScaleModels.OrdersWeighings;

public sealed class WsSqlOrderWeightingRepository : WsSqlTableRepositoryBase<WsSqlOrderWeighingModel>
{
    public List<WsSqlOrderWeighingModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(new(nameof(WsSqlTableBase.ChangeDt), WsSqlEnumOrder.Desc));
        return SqlCore.GetListNotNullable<WsSqlOrderWeighingModel>(sqlCrudConfig);
    }

}