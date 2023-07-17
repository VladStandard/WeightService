using WsStorageCore.Views.ViewScaleModels.WebLogs;

namespace WsStorageCoreTests.Views.ViewScaleModels.WebLogs;

[TestFixture]
public sealed class ViewWebLogsRepositoryTests : ViewRepositoryTests
{
    private IViewWebLogRepository ViewWebLogRepository = WsSqlViewWebLogRepository.Instance;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewWebLogModel> items = ViewWebLogRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}