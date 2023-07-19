using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleModels.Orders;

// TODO: get data for OrderRepository
[TestFixture]
public sealed class OrderRepositoryTests : TableRepositoryTests
{
    private WsSqlOrderRepository OrderRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlOrderModel> items = OrderRepository.GetList(SqlCrudConfig);
            // Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}