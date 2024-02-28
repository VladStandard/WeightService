using Ws.Database.Core.Common.Commands;
using Ws.Database.Core.Common.Queries.Item;
using Ws.Database.Core.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Warehouses;

public sealed class SqlWarehouseRepository : BaseRepository, IGetItemByUid<WarehouseEntity>, IGetAll<WarehouseEntity>,
    ISave<WarehouseEntity>, IUpdate<WarehouseEntity>
{
    public WarehouseEntity GetByUid(Guid uid) => Session.Get<WarehouseEntity>(uid) ?? new();
    public IEnumerable<WarehouseEntity> GetAll() => Session.Query<WarehouseEntity>().OrderBy(i => i.Name).ToList();
    public WarehouseEntity Save(WarehouseEntity item) => (Session.Save(item) as WarehouseEntity)!;
    public WarehouseEntity Update(WarehouseEntity item) { Session.Update(item); return item; }
}