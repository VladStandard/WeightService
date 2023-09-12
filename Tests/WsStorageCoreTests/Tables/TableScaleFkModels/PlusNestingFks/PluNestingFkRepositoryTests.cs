using WsStorageCore.Tables.TableScaleFkModels.PlusNestingFks;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusNestingFks;

[TestFixture]
public sealed class PluNestingFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluNestingFkRepository PluNestingFkRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue =>
        Is.Ordered.Using((IComparer<WsSqlPluNestingFkModel>)Comparer<WsSqlPluNestingFkModel>.Create((x, y) =>
            x.Plu.Number.CompareTo(y.Plu.Number))).Ascending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlPluNestingFkModel> items = PluNestingFkRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
    
}