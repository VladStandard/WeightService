using WsStorageCore.Views.ViewScaleModels.WebLogs;

namespace WsStorageCoreTests.Views.ViewScaleModels.WebLogs;

[TestFixture]
public sealed class ViewWebLogsRepositoryTests : ViewRepositoryTests
{
    private IViewWebLogRepository ViewWebLogRepository { get; } = new WsSqlViewWebLogRepository();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewWebLogModel> items = ViewWebLogRepository.GetList(SqlCrudConfig);
            WsTestsUtils.DataTests.PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}