using Ws.StorageCore.Entities.SchemaScale.PlusStorageMethods;

namespace Ws.StorageCoreTests.Tables.TableScaleModels.PluStorageMethods;

[TestFixture]
public sealed class PluStorageRepositoryTests : TableRepositoryTests
{
    private SqlPluStorageMethodRepository PluStorageMethodRepository { get; } = new();

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlPluStorageMethodEntity> items = PluStorageMethodRepository.GetList();
            ParseRecords(items);
        }, false);
    }
}