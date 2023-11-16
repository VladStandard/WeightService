using Ws.StorageCore.Views.ViewPrintModels.PluLabelAggr;

namespace Ws.StorageCoreTests.Views.ViewPrintModels.PluLabelAggr;

[TestFixture]
public sealed class ViewPluLabelAggrRepositoryTests : ViewRepositoryTests
{
    private IViewPluLabelAggrRepository ViewPluLabelAggrRepository { get; } = new SqlViewPluLabelAggrRepository();

    protected override IResolveConstraint SortOrderValue => Is
        .Ordered.By(nameof(SqlViewPluLabelAggrModel.CreateDt)).Descending;

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            List<SqlViewPluLabelAggrModel> items = ViewPluLabelAggrRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            PrintViewRecords(items);
        }, false);
    }
}