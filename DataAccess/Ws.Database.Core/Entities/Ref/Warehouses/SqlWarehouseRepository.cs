using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Warehouses;

public sealed class SqlWarehouseRepository : IUidRepo<WarehouseEntity>
{
    public WarehouseEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<WarehouseEntity>(uid);
    
    public IEnumerable<WarehouseEntity> GetEnumerable()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCoreHelper.Instance.GetEnumerable<WarehouseEntity>(crud);
    }
}