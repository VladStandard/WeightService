namespace WsStorageCore.Tables.TableScaleModels.ProductSeries;

public class WsSqlProductSeriesRepository: WsSqlTableRepositoryBase<WsSqlProductSeriesModel>
{
    public List<WsSqlProductSeriesModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.CreateDt), Direction = WsSqlEnumOrder.Desc });
        List<WsSqlProductSeriesModel> list = SqlCore.GetListNotNullable<WsSqlProductSeriesModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderByDescending(item => item.CreateDt).ToList();
        return list;
    }
}