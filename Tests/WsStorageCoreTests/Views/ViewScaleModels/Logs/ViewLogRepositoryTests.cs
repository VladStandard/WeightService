using NUnit.Framework.Constraints;
using WsStorageCore.Views.ViewScaleModels.Logs;

namespace WsStorageCoreTests.Views.ViewScaleModels.Logs;

[TestFixture]
public sealed class ViewLogRepositoryTests : ViewRepositoryTests
{
    private IViewLogRepository ViewLogRepository { get; } = new WsSqlViewLogRepository();

    protected override IResolveConstraint SortOrderValue => Is
        .Ordered.By(nameof(WsSqlViewLogModel.CreateDt)).Descending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewLogModel> items = ViewLogRepository.GetList(SqlCrudConfig);
            PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}