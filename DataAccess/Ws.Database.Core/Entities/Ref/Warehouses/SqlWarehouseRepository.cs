using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Warehouses;

public sealed class SqlWarehouseRepository :  BaseRepository, IGetItemByUid<WarehouseEntity>, IGetAll<WarehouseEntity>
{
    public WarehouseEntity GetByUid(Guid uid) => Session.Get<WarehouseEntity>(uid) ?? new();
    public IEnumerable<WarehouseEntity> GetAll() => Session.Query<WarehouseEntity>().OrderBy(i => i.Name).ToList();
}