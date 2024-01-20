using Ws.StorageCore.Entities.SchemaRef.Warehouses;

namespace Ws.StorageCoreTests.Tables.TableRefModels.Warehouses;

[TestFixture]
public sealed class WarehouseRepositoryTests : TableRepositoryTests
{
    private SqlWarehouseRepository WarehousesRepository { get; } = new();

    [Test, Order(1)]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlWarehouseEntity> items = WarehousesRepository.GetEnumerable();
            ParseRecords(items);
        });
    }
}