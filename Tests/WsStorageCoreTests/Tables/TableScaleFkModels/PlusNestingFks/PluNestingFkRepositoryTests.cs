using WsStorageCore.Tables.TableScaleFkModels.PlusNestingFks;
using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusNestingFks;

public sealed class PluNestingFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluNestingFkRepository PluNestingFkRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluNestingFkModel> items = PluNestingFkRepository.GetList(SqlCrudConfig);
            WsTestsUtils.DataTests.ParseRecords(items);
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
            WsTestsUtils.DataTests.PrintViewRecords(plusNestingFks);
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
            WsTestsUtils.DataTests.ParseRecords(plusNestingFks);
        }, false, new() { WsEnumConfiguration.ReleaseVS, WsEnumConfiguration.DevelopVS });
    }
}