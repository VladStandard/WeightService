using WsStorageCore.Views.ViewRefModels.PluLines;

namespace WsStorageCoreTests.Views.ViewRefModels.PluLines;

public class ViewPluLineRepositoryTests : ViewRepositoryTests
{
    private IViewPluLineRepository ViewPluLineRepository { get; } = new WsSqlViewPluLineRepository();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewPluLineModel> items = ViewPluLineRepository.GetList(SqlCrudConfig);
            WsTestsUtils.DataTests.PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}