using NUnit.Framework.Constraints;
using WsStorageCore.Views.ViewDiagModels.LogsMemory;

namespace WsStorageCoreTests.Views.ViewDiagModels.LogsMemory;

[TestFixture]
public sealed class ViewLogMemoryRepositoryTests : ViewRepositoryTests
{
    private IViewLogMemoryRepository ViewLogMemoryRepository { get; } = new WsSqlViewLogMemoryRepository();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(WsSqlViewLogMemoryModel.CreateDt)).Descending;
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewLogMemoryModel> items = ViewLogMemoryRepository.GetList(SqlCrudConfig);
            PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}