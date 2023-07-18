using WsStorageCore.Tables.TableScaleFkModels.PlusNestingFks;

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
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}