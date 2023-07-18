namespace WsStorageCore.Tables.TableScaleModels.ProductSeries;

public class WsSqlProductSeriesRepository: WsSqlTableRepositoryBase<WsSqlProductSeriesModel>
{
    public List<WsSqlProductSeriesModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => ContextList.GetListNotNullableProductSeries(sqlCrudConfig);
}