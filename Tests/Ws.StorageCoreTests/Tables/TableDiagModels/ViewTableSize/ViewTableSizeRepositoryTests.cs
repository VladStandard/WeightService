using Ws.StorageCore.Entities.SchemaDiag.TableSize;

namespace Ws.StorageCoreTests.Tables.TableDiagModels.ViewTableSize;

[TestFixture]
public sealed class ViewTableSizeRepositoryTests : TableRepositoryTests
{
    private SqlViewTableSizeRepository TableSizeRepository { get; set; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(SqlViewTableSizeModel.UsedSpaceMb)).Descending;

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlViewTableSizeModel> items = TableSizeRepository.GetEnumerable();
            ParseRecords(items);
        }, false);
    }
}