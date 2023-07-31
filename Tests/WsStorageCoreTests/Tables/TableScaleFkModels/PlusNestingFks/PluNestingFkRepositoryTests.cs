using NUnit.Framework.Constraints;
using WsStorageCore.Tables.TableScaleFkModels.PlusNestingFks;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusNestingFks;

[TestFixture]
public sealed class PluNestingFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluNestingFkRepository PluNestingFkRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue =>
        Is.Ordered.Using((IComparer<WsSqlPluNestingFkModel>)Comparer<WsSqlPluNestingFkModel>.Create((x, y) =>
            x.PluBundle.Plu.Number.CompareTo(y.PluBundle.Plu.Number))).Ascending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluNestingFkModel> items = PluNestingFkRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultPublishTypes);
    }

    [Test]
    public void GetListByPluNumber()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluNestingFkModel> plusNestingFks = PluNestingFkRepository.GetListByPluNumber(312);
            foreach (WsSqlPluNestingFkModel pluNestingFk in plusNestingFks)
                Assert.That(pluNestingFk.PluBundle.Plu.Number, Is.EqualTo(312));
            ParseRecords(plusNestingFks);
        }, false, new() { WsEnumConfiguration.ReleaseVS, WsEnumConfiguration.DevelopVS });
    }

    [Test]
    public void GetListByPluUid()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            Guid uid = Guid.Parse("99A2AD82-63BB-4FC0-B5E1-1DE2AD86BD0F");
            List<WsSqlPluNestingFkModel> plusNestingFks = PluNestingFkRepository.GetListByPluUid(uid);
            foreach (WsSqlPluNestingFkModel pluNestingFk in plusNestingFks)
                Assert.That(pluNestingFk.PluBundle.Plu.IdentityValueUid, Is.EqualTo(uid));
            ParseRecords(plusNestingFks);
        }, false, new() { WsEnumConfiguration.ReleaseVS, WsEnumConfiguration.DevelopVS });
    }
}