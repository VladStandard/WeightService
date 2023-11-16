using WsStorageCore.Views.ViewRefModels.PluStorageMethods;

namespace WsStorageCoreTests.Views.ViewRefModels.PluStorageMethods;

public class ViewPluStorageRepository : ViewRepositoryTests
{
    private IViewStorageMethodsRepository ViewPluStorageMethodRepository { get; } =
        new SqlViewPluStorageMethodRepository();

    protected override IResolveConstraint SortOrderValue => Is
        .Ordered.By(nameof(SqlViewPluStorageMethodModel.PluNumber)).Ascending
        .Then.By(nameof(SqlViewPluStorageMethodModel.PluName)).Ascending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<SqlViewPluStorageMethodModel> items = ViewPluStorageMethodRepository.GetList(SqlCrudConfig);
            PrintViewRecords(items);
        }, false);
    }
}