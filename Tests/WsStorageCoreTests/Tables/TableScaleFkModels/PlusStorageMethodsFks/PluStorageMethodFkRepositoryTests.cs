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
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}