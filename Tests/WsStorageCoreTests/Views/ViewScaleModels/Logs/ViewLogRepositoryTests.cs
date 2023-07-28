// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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