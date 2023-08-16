using WsStorageCore.Views.ViewRefModels.PluLines;

namespace WsStorageCoreTests.Views.ViewRefModels.PluLines;

public class ViewPluLineRepositoryTests : ViewRepositoryTests
{
    private IViewPluLineRepository ViewPluLineRepository { get; } = new WsSqlViewPluLineRepository();

    protected override IResolveConstraint SortOrderValue => Is
        .Ordered.By(nameof(WsSqlViewPluLineModel.ScaleId)).Ascending
        .Then.By(nameof(WsSqlViewPluLineModel.PluNumber)).Ascending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlViewPluLineModel> items = ViewPluLineRepository.GetEnumerable(SqlCrudConfig);
            PrintViewRecords(items);
        }, false, AllConfigurations);
    }
}