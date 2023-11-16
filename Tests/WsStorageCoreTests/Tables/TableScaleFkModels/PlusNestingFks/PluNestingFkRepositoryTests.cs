using WsStorageCore.Entities.SchemaScale.PlusNestingFks;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusNestingFks;

[TestFixture]
public sealed class PluNestingFkRepositoryTests : TableRepositoryTests
{
    private SqlPluNestingFkRepository PluNestingFkRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue =>
        Is.Ordered.Using((IComparer<SqlPluNestingFkEntity>)Comparer<SqlPluNestingFkEntity>.Create((x, y) =>
            x.Plu.Number.CompareTo(y.Plu.Number))).Ascending;

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlPluNestingFkEntity> items = PluNestingFkRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
    
}