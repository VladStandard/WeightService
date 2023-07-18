namespace WsStorageCoreTests.Tables.TableScaleModels.OrdersWeighings;

[TestFixture]
public sealed class OrderWeightingRepositoryTests : TableRepositoryTests
{
    private WsSqlOrderWeightingRepository OrderWeightingRepository  { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlOrderWeighingModel> items = OrderWeightingRepository.GetList(SqlCrudConfig);
            // Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}