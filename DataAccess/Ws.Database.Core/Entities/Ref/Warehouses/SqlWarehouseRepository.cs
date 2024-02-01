using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Warehouses;

public sealed class SqlWarehouseRepository : IGetItemByUid<WarehouseEntity>, IGetAll<WarehouseEntity>
{
    public WarehouseEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<WarehouseEntity>(uid);
    
    public IEnumerable<WarehouseEntity> GetAll()
    {
        return SqlCoreHelper.Instance.GetEnumerable(
            QueryOver.Of<WarehouseEntity>().OrderBy(i => i.Name).Asc
        );
    }
}