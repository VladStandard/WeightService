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
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}