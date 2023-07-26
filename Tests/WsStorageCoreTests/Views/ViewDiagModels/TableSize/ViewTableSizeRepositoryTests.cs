using NUnit.Framework.Constraints;
using WsStorageCore.Views.ViewDiagModels.TableSize;

namespace WsStorageCoreTests.Views.ViewDiagModels.TableSize;

[TestFixture]
public sealed class ViewTableSizeRepositoryTests : ViewRepositoryTests
{
    private IViewTableSizeRepository ViewTableSizeRepository { get; } = new WsSqlViewTableSizeRepository();
    protected override CollectionOrderedConstraint SortOrderValue => Is.Ordered.By(nameof(WsSqlViewTableSizeModel.UsedSpaceMb)).Descending;
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewTableSizeModel> items = ViewTableSizeRepository.GetList(SqlCrudConfig);
            PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}