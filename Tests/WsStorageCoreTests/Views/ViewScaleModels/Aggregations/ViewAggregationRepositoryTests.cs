using WsStorageCore.Views.ViewScaleModels.Aggregations;

namespace WsStorageCoreTests.Views.ViewScaleModels.Aggregations;

[TestFixture]
public sealed class ViewAggregationsRepositoryTests
{
    private WsSqlViewWeightingAggrRepository ViewWeightingAggrRepository = WsSqlViewWeightingAggrRepository.Instance;
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewWeightingAggrModel> items = ViewWeightingAggrRepository.GetList();
            Assert.That(items.Any(), Is.True);
            // WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}