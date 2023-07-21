using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusStorageMethodsFks;

public sealed class PluStorageMethodsFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluStorageMethodFkRepository PluStorageMethodFkRepository { get; set; } = new();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluStorageMethodFkModel> items = PluStorageMethodFkRepository.GetList(SqlCrudConfig);
            WsTestsUtils.DataTests.ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
}