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
            IEnumerable<WsSqlPluCharacteristicsFkModel> items = PluCharacteristicsFkRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}