// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework.Constraints;
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
            List<WsSqlViewWebLogModel> items = ViewWebLogRepository.GetList(SqlCrudConfig);
            PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}