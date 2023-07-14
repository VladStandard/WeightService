using WsStorageCore.ViewDiagModels.LogsMemory;

namespace WsStorageCoreTests.ViewDiagModels.LogsMemory;

[TestFixture]
public sealed class ViewLogMemoryRepositoryTests
{
    private WsSqlViewLogMemoryRepository ViewLogMemoryRepository = WsSqlViewLogMemoryRepository.Instance;
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewLogMemoryModel> items = ViewLogMemoryRepository.GetList(new WsSqlCrudConfigModel());
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}