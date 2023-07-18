using WsStorageCore.Tables.TableScaleFkModels.PlusFks;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusFks;

public sealed class PluFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluFkRepository PluFkRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluFkModel> items = PluFkRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}