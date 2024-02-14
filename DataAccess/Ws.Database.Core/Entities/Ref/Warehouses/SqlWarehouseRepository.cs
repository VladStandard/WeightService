using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Warehouses;

public sealed class SqlWarehouseRepository :  BaseRepository, IGetItemByUid<WarehouseEntity>, IGetAll<WarehouseEntity>
{
    public WarehouseEntity GetByUid(Guid uid) => SqlCoreHelper.GetItemById<WarehouseEntity>(uid);
    
    public IEnumerable<WarehouseEntity> GetAll()
    {
        return SqlCoreHelper.GetEnumerable(
            QueryOver.Of<WarehouseEntity>().OrderBy(i => i.Name).Asc
        );
    }
}