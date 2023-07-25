using WsStorageCoreTests.Tables.Common;

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
            ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
}