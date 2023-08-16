namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusGroupsFks;

[TestFixture]
public sealed class PluGroupFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluGroupFkRepository PluGroupFkRepository { get; } = new();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlPluGroupFkModel> items = PluGroupFkRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}