using Ws.Domain.Models.Entities.Ref;
using Ws.StorageCore.OrmUtils;

namespace Ws.StorageCore.Entities.Ref.Warehouses;

public sealed class SqlWarehouseRepository : SqlTableRepositoryBase<WarehouseEntity>
{
    public IEnumerable<WarehouseEntity> GetEnumerable()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<WarehouseEntity>(crud);
    }
}