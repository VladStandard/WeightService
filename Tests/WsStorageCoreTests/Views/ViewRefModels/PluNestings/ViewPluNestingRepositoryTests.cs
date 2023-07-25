using WsStorageCore.Views.ViewRefModels.PluNestings;

namespace WsStorageCoreTests.Views.ViewRefModels.PluNestings;

public class ViewPluNestingRepositoryTests : ViewRepositoryTests
{
    private IViewPluNestingRepository ViewPluNestingRepository { get; } = new WsSqlViewPluNestingRepository();

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