using WsStorageCore.Views.ViewScaleModels.WebLogs;

namespace WsStorageCoreTests.Views.ViewScaleModels.WebLogs;

[TestFixture]
public sealed class ViewWebLogsRepositoryTests
{
    private WsSqlViewWebLogRepository ViewWebLogRepository = WsSqlViewWebLogRepository.Instance;
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlCrudConfigModel sqlConfig = new WsSqlCrudConfigModel() { SelectTopRowsCount = 10 };
            List<WsSqlViewWebLogModel> items = ViewWebLogRepository.GetList(sqlConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}