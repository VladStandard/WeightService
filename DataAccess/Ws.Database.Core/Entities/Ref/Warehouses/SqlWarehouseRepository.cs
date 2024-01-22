using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Warehouses;

public sealed class SqlWarehouseRepository : SqlTableRepositoryBase<WarehouseEntity>
{
    public IEnumerable<WarehouseEntity> GetEnumerable()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<WarehouseEntity>(crud);
    }
}