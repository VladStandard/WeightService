using WsStorageCore.Views.ViewDiagModels.TableSize;

namespace WsStorageCoreTests.Views.ViewDiagModels.TableSize;

[TestFixture]
public sealed class ViewTableSizeRepositoryTests
{
    private WsSqlViewTableSizeRepository ViewTableSizeRepository = WsSqlViewTableSizeRepository.Instance;
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewTableSizeModel> items = ViewTableSizeRepository.GetList();
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}