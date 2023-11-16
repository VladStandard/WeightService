using Ws.StorageCore.Views.ViewScaleModels.PluWeightings;

namespace Ws.StorageCoreTests.Views.ViewScaleModels.PluWeightings;

[TestFixture]
public sealed class ViewPluWeightingRepositoryTests : ViewRepositoryTests
{
    private IViewPluWeightingRepository ViewPluWeightingRepository { get; } = new SqlViewPluWeightingRepository();

    protected override IResolveConstraint SortOrderValue => Is
        .Ordered.By(nameof(SqlViewPluWeightingModel.CreateDt)).Descending;

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IList<SqlViewPluWeightingModel> items = ViewPluWeightingRepository.GetList(SqlCrudConfig);
            PrintViewRecords(items);
        }, false);
    }
}