using WsStorageCore.Views.ViewScaleModels.WebLogs;

namespace WsStorageCoreTests.Views.ViewScaleModels.WebLogs;

[TestFixture]
public sealed class ViewWebLogsRepositoryTests : ViewRepositoryTests
{
    private IViewWebLogRepository ViewWebLogRepository { get; } = new WsSqlViewWebLogRepository();

    protected override IResolveConstraint SortOrderValue => Is
        .Ordered.By(nameof(WsSqlViewWebLogModel.CreateDt)).Descending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IList<WsSqlViewWebLogModel> items = ViewWebLogRepository.GetList(SqlCrudConfig);
            PrintViewRecords(items);
        }, false, AllConfigurations);
    }
}