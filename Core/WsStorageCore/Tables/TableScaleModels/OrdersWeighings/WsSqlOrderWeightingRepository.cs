namespace WsStorageCore.Tables.TableScaleModels.OrdersWeighings;

public sealed class WsSqlOrderWeightingRepository : WsSqlTableRepositoryBase<WsSqlOrderWeighingModel>
{
    public List<WsSqlOrderWeighingModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        return SqlCore.GetEnumerableNotNullable<WsSqlOrderWeighingModel>(sqlCrudConfig).ToList();
    }

}