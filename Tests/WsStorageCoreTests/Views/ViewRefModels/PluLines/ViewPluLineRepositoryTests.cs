using WsStorageCore.Views.ViewRefModels.PluLines;

namespace WsStorageCoreTests.Views.ViewRefModels.PluLines;

public class ViewPluLineRepositoryTests
{
    private WsSqlViewPluLineRepository ViewPluLineRepository = WsSqlViewPluLineRepository.Instance;
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewPluLineModel> items = ViewPluLineRepository.GetList();
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}