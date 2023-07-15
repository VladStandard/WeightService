using WsStorageCore.ViewRefModels.PluLines;

namespace WsStorageCoreTests.ViewRefModels.PluLines;

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