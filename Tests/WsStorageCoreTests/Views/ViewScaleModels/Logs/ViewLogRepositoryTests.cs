using WsStorageCore.Views.ViewScaleModels.Logs;

namespace WsStorageCoreTests.Views.ViewScaleModels.Logs;

[TestFixture]
public sealed class ViewLogRepositoryTests : ViewRepositoryTests
{
    private IViewLogRepository ViewLogRepository = WsSqlViewLogRepository.Instance;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewLogModel> items = ViewLogRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}