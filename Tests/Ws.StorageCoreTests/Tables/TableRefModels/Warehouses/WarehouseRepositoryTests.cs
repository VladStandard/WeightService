using Ws.Domain.Models.Entities.Ref;
using Ws.StorageCore.Entities.Ref.Warehouses;

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
            IEnumerable<WarehouseEntity> items = WarehousesRepository.GetEnumerable();
            ParseRecords(items);
        });
    }
}