using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusCharacteristicsFks;

public sealed class PluCharacteristicsFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluCharacteristicsFkRepository PluCharacteristicsFkRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluCharacteristicsFkModel> items = PluCharacteristicsFkRepository.GetList(SqlCrudConfig);
            WsTestsUtils.DataTests.ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
}