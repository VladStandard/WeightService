using WsStorageCore.ViewRefModels.PluStorageMethods;

namespace WsStorageCoreTests.ViewRefModels.PluStorageMethods;

public class ViewPluStorageRepository
{
    private WsSqlViewPluStorageMethodRepository ViewPluStorageMethodRepository = WsSqlViewPluStorageMethodRepository.Instance;
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewPluStorageMethodModel> items = ViewPluStorageMethodRepository.GetList();
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}