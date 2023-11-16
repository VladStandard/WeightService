using Ws.StorageCore.Views.ViewRefModels.PluLines;

namespace Ws.StorageCoreTests.Views.ViewRefModels.PluLines;

public class ViewPluLineRepositoryTests : ViewRepositoryTests
{
    private IViewPluLineRepository ViewPluLineRepository { get; } = new SqlViewPluLineRepository();

    protected override IResolveConstraint SortOrderValue => Is
        .Ordered.By(nameof(SqlViewPluLineModel.ScaleId)).Ascending
        .Then.By(nameof(SqlViewPluLineModel.PluNumber)).Ascending;

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlViewPluLineModel> items = ViewPluLineRepository.GetEnumerable(SqlCrudConfig);
            PrintViewRecords(items);
        }, false);
    }
}