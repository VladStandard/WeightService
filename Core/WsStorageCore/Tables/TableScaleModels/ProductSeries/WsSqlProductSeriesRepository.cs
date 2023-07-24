namespace WsStorageCore.Tables.TableScaleModels.ProductSeries;

public class WsSqlProductSeriesRepository: WsSqlTableRepositoryBase<WsSqlProductSeriesModel>
{
    public WsSqlProductSeriesModel GetItemByLineNotClose(WsSqlScaleModel line)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            new List<WsSqlFieldFilterModel>
            {
                new() { Name = nameof(WsSqlProductSeriesModel.IsClose), Value = false },
                new() { Name = $"{nameof(WsSqlProductSeriesModel.Scale)}.{nameof(WsSqlScaleModel.IdentityValueId)}",
                    Value = line.IdentityValueId }
            }, WsSqlEnumIsMarked.ShowAll, false);
        return SqlCore.GetItemNullable<WsSqlProductSeriesModel>(sqlCrudConfig) ?? 
               SqlCore.GetItemNewEmpty<WsSqlProductSeriesModel>();
    }
    
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