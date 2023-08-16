namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusBrandFks;

[TestFixture]
public sealed class PluBrandFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluBrandFkRepository PluBrandFkRepository { get; } = new();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlPluBrandFkModel> items = PluBrandFkRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}