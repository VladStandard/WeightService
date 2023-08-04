using WsStorageCore.Views.ViewScaleModels.Aggregations;

namespace WsStorageCoreTests.Views.ViewScaleModels.Aggregations;

[TestFixture]
public sealed class ViewAggregationsRepositoryTests : ViewRepositoryTests
{
    private IViewWeightingAggrRepository ViewWeightingAggrRepository { get; } = new WsSqlViewWeightingAggrRepository();

    protected override IResolveConstraint SortOrderValue => Is
        .Ordered.By(nameof(WsSqlViewWeightingAggrModel.ChangeDt)).Descending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewWeightingAggrModel> items = ViewWeightingAggrRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}