using Ws.StorageCore.Entities.SchemaRef.PlusLines;

namespace Ws.StorageCoreTests.Tables.TableRefModels.PluLines;

[TestFixture]
public sealed class PluLinesRepositoryTests : TableRepositoryTests
{
    private SqlPluLineRepository PluLineRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue =>
        Is.Ordered.Using(
            (IComparer<SqlPluLineEntity>)Comparer<SqlPluLineEntity>.Create((x, y) =>
                x.Plu.Number.CompareTo(y.Plu.Number))).Ascending;

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            List<SqlPluLineEntity> items = PluLineRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
}