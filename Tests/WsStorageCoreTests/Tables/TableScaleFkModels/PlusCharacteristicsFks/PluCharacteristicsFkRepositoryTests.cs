namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusCharacteristicsFks;

[TestFixture]
public sealed class PluCharacteristicsFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluCharacteristicsFkRepository PluCharacteristicsFkRepository { get; } = new();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluCharacteristicsFkModel> items = PluCharacteristicsFkRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}