using NUnit.Framework.Constraints;
using WsStorageCore.Views.ViewRefModels.PluNestings;

namespace WsStorageCoreTests.Views.ViewRefModels.PluNestings;

public class ViewPluNestingRepositoryTests : ViewRepositoryTests
{
    private IViewPluNestingRepository ViewPluNestingRepository { get; } = new WsSqlViewPluNestingRepository();

    protected override IResolveConstraint SortOrderValue => Is
        .Ordered.By(nameof(WsSqlViewPluNestingModel.PluNumber)).Ascending
        .Then.By(nameof(WsSqlViewPluNestingModel.PluName)).Ascending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewPluNestingModel> items = ViewPluNestingRepository.GetList();
            PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}