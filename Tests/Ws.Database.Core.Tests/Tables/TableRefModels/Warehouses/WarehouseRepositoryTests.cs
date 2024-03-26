using Ws.Database.Nhibernate.Entities.Ref.Warehouses;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.StorageCoreTests.Tables.TableRefModels.Warehouses;

[TestFixture]
public sealed class WarehouseRepositoryTests : TableRepositoryTests
{
    private SqlWarehouseRepository WarehousesRepository { get; } = new();

    [Test, Order(1)]
    public void GetList()
    {
        AssertAction(() =>
        {
            IEnumerable<WarehouseEntity> items = WarehousesRepository.GetAll();
            ParseRecords(items);
        });
    }
}