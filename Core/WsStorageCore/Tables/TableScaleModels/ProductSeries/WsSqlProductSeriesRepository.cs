using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableScaleModels.ProductSeries;

public class WsSqlProductSeriesRepository: WsSqlTableRepositoryBase<WsSqlProductSeriesModel>
{
    public WsSqlProductSeriesModel GetItemByLineNotClose(WsSqlScaleModel line)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilters(new() {
            SqlRestrictions.Equal(nameof(WsSqlProductSeriesModel.IsClose), false),
            SqlRestrictions.EqualFk(nameof(WsSqlProductSeriesModel.Scale), line)
        });
        return SqlCore.GetItemByCrud<WsSqlProductSeriesModel>(sqlCrudConfig);
    }
    
    public List<WsSqlProductSeriesModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetEnumerable<WsSqlProductSeriesModel>(sqlCrudConfig).ToList();
    }
}