using WsStorageCore.Entities.SchemaScale.PlusFks;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusFks;

[TestFixture]
public sealed class PluFkRepositoryTests : TableRepositoryTests
{
    private SqlPluFkRepository PluFkRepository { get; } = new();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlPluFkEntity> items = PluFkRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
}