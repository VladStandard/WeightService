namespace WsStorageCore.Tables.TableScaleModels.ProductSeries;

public class WsSqlProductSeriesRepository: WsSqlTableRepositoryBase<WsSqlProductSeriesModel>
{
    public WsSqlProductSeriesModel GetItemByLineNotClose(WsSqlScaleModel line)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(new() { Name = nameof(WsSqlProductSeriesModel.IsClose), Value = false });
        sqlCrudConfig.AddFkIdentityFilter(nameof(WsSqlProductSeriesModel.Scale), line);
        return SqlCore.GetItemByCrud<WsSqlProductSeriesModel>(sqlCrudConfig);
    }
    
    public List<WsSqlProductSeriesModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(nameof(WsSqlTableBase.CreateDt), WsSqlEnumOrder.Desc);
        return SqlCore.GetEnumerableNotNullable<WsSqlProductSeriesModel>(sqlCrudConfig).ToList();
    }
}