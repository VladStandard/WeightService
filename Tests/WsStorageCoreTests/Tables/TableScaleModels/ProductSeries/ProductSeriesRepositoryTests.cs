using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleModels.ProductSeries;

[TestFixture]
public sealed class ProductSeriesRepositoryTests : TableRepositoryTests
{
    private WsSqlProductSeriesRepository ProductSeriesRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlProductSeriesModel> items = ProductSeriesRepository.GetList(SqlCrudConfig);
            WsTestsUtils.DataTests.ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
}