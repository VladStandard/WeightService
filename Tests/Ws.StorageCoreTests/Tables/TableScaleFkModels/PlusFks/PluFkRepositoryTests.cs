using Ws.StorageCore.Entities.SchemaScale.PlusFks;

namespace Ws.StorageCoreTests.Tables.TableScaleFkModels.PlusFks;

[TestFixture]
public sealed class PluFkRepositoryTests : TableRepositoryTests
{
    private SqlPluFkRepository PluFkRepository { get; } = new();

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlPluFkEntity> items = PluFkRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
}