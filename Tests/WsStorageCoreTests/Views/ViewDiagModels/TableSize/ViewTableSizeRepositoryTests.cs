// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework.Constraints;
using WsStorageCore.Views.ViewDiagModels.TableSize;

namespace WsStorageCoreTests.Views.ViewDiagModels.TableSize;

[TestFixture]
public sealed class ViewTableSizeRepositoryTests : ViewRepositoryTests
{
    private IViewTableSizeRepository ViewTableSizeRepository { get; } = new WsSqlViewTableSizeRepository();

    protected override CollectionOrderedConstraint SortOrderValue =>
        Is.Ordered.By(nameof(WsSqlViewTableSizeModel.UsedSpaceMb)).Descending;

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