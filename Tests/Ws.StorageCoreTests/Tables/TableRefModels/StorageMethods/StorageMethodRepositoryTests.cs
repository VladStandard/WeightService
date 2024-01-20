using Ws.Domain.Models.Entities.Ref;
using Ws.StorageCore.Entities.Ref.StorageMethods;

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
            IEnumerable<StorageMethodEntity> items = StorageMethodRepository.GetList();
            ParseRecords(items);
        });
    }
}