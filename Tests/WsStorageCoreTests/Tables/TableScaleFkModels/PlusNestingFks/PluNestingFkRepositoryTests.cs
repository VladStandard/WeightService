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

    [Test]
    public void GetListByPluNumber()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlPluNestingFkModel> plusNestingFks = PluNestingFkRepository.GetEnumerableByPluNumber(312).ToList();
            foreach (WsSqlPluNestingFkModel pluNestingFk in plusNestingFks)
                Assert.That(pluNestingFk.Plu.Number, Is.EqualTo(312));
            ParseRecords(plusNestingFks);
        }, false, new() { WsEnumConfiguration.ReleaseVS, WsEnumConfiguration.DevelopVS });
    }

    [Test]
    public void GetListByPluUid()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            Guid uid = Guid.Parse("99A2AD82-63BB-4FC0-B5E1-1DE2AD86BD0F");
            IEnumerable<WsSqlPluNestingFkModel> plusNestingFks = PluNestingFkRepository.GetEnumerableByPluUid(uid).ToList();
            foreach (WsSqlPluNestingFkModel pluNestingFk in plusNestingFks)
                Assert.That(pluNestingFk.Plu.IdentityValueUid, Is.EqualTo(uid));
            ParseRecords(plusNestingFks);
        }, false, new() { WsEnumConfiguration.ReleaseVS, WsEnumConfiguration.DevelopVS });
    }
}