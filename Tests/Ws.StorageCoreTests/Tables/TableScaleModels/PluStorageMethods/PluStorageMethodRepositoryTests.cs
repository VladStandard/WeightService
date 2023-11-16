using Ws.StorageCoreTests.Tables.Common;
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
            List<SqlPluStorageMethodEntity> items = PluStorageMethodRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
}