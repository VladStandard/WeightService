using WsStorageCore.Views.ViewPrintModels.PluLabelAggr;

namespace WsStorageCoreTests.Views.ViewPrintModels.PluLabelAggr;

[TestFixture]
public sealed class ViewPluLabelAggrRepositoryTests : ViewRepositoryTests
{
    private IViewPluLabelAggrRepository ViewPluLabelAggrRepository { get; } = new WsSqlViewPluLabelAggrRepository();

    protected override IResolveConstraint SortOrderValue => Is
        .Ordered.By(nameof(WsSqlViewPluLabelAggrModel.CreateDt)).Descending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewPluLabelAggrModel> items = ViewPluLabelAggrRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            PrintViewRecords(items);
        }, false);
    }
}