using Ws.StorageCore.Views.ViewRefModels.PluStorageMethods;

namespace Ws.StorageCoreTests.Views.ViewRefModels.PluStorageMethods;

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
        TestsUtils.DataTests.AssertAction(() =>
        {
            List<SqlViewPluStorageMethodModel> items = ViewPluStorageMethodRepository.GetList(SqlCrudConfig);
            PrintViewRecords(items);
        }, false);
    }
}