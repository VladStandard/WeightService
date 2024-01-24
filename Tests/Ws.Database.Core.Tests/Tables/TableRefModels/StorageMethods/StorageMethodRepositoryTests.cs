using Ws.Database.Core.Entities.Ref.StorageMethods;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.StorageCoreTests.Tables.TableRefModels.StorageMethods;

[TestFixture]
public sealed class PluStorageRepositoryTests : TableRepositoryTests
{
    private SqlStorageMethodRepository StorageMethodRepository { get; } = new();

    [Test]
    public void GetList()
    {
        AssertAction(() =>
        {
            IEnumerable<StorageMethodEntity> items = StorageMethodRepository.GetList();
            ParseRecords(items);
        });
    }
}