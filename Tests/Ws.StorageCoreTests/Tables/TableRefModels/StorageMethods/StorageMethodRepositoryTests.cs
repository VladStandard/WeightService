using Ws.StorageCore.Entities.SchemaRef.StorageMethods;

namespace Ws.StorageCoreTests.Tables.TableRefModels.StorageMethods;

[TestFixture]
public sealed class PluStorageRepositoryTests : TableRepositoryTests
{
    private SqlStorageMethodRepository StorageMethodRepository { get; } = new();

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlStorageMethodEntity> items = StorageMethodRepository.GetList();
            ParseRecords(items);
        }, false);
    }
}