using WsStorageCore.Views.ViewDiagModels.LogsMemory;

namespace WsStorageCoreTests.Views.ViewDiagModels.LogsMemory;

[TestFixture]
public sealed class ViewLogMemoryRepositoryTests : ViewRepositoryTests
{
    private IViewLogMemoryRepository ViewLogMemoryRepository = WsSqlViewLogMemoryRepository.Instance;
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewLogMemoryModel> items = ViewLogMemoryRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}