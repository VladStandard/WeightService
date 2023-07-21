using WsStorageCore.Views.ViewScaleModels.Logs;

namespace WsStorageCoreTests.Views.ViewScaleModels.Logs;

[TestFixture]
public sealed class ViewLogRepositoryTests : ViewRepositoryTests
{
    private IViewLogRepository ViewLogRepository { get; } = new WsSqlViewLogRepository();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewLogModel> items = ViewLogRepository.GetList(SqlCrudConfig);
            WsTestsUtils.DataTests.PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}