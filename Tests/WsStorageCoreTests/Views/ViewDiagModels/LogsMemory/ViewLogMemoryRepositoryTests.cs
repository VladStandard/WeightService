using WsStorageCore.Views.ViewDiagModels.LogsMemory;

namespace WsStorageCoreTests.Views.ViewDiagModels.LogsMemory;

[TestFixture]
public sealed class ViewLogMemoryRepositoryTests : ViewRepositoryTests
{
    private IViewLogMemoryRepository ViewLogMemoryRepository { get; } = new WsSqlViewLogMemoryRepository();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewLogMemoryModel> items = ViewLogMemoryRepository.GetList(SqlCrudConfig);
            WsTestsUtils.DataTests.PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}