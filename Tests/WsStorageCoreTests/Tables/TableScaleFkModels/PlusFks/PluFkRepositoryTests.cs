using WsStorageCore.Tables.TableScaleFkModels.PlusFks;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusFks;

[TestFixture]
public sealed class PluFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluFkRepository PluFkRepository { get; } = new();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlPluFkModel> items = PluFkRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}