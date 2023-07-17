using WsStorageCore.Views.ViewScaleModels.Logs;

namespace WsStorageCoreTests.Views.ViewScaleModels.Logs;

[TestFixture]
public sealed class ViewLogRepositoryTests
{
    private WsSqlViewLogRepository ViewLogRepository = WsSqlViewLogRepository.Instance;
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlCrudConfigModel sqlConfig = new WsSqlCrudConfigModel() { SelectTopRowsCount = 10 };
            List<WsSqlViewLogModel> items = ViewLogRepository.GetList(sqlConfig, null, null);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}