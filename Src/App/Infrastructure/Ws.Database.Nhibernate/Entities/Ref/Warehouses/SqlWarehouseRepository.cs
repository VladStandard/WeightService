using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Nhibernate.Entities.Ref.Warehouses;

public sealed class SqlWarehouseRepository : BaseRepository, IGetItemByUid<WarehouseEntity>, IGetAll<WarehouseEntity>,
    ISave<WarehouseEntity>, IUpdate<WarehouseEntity>, IDelete<WarehouseEntity>
{
    public WarehouseEntity GetByUid(Guid uid) => Session.Get<WarehouseEntity>(uid) ?? new();
    public IEnumerable<WarehouseEntity> GetAll() => Session.Query<WarehouseEntity>().OrderBy(i => i.Name).ToList();
    public WarehouseEntity Save(WarehouseEntity item) { Session.Save(item); return item; }
    public WarehouseEntity Update(WarehouseEntity item) { Session.Update(item); return item; }
    public void Delete(WarehouseEntity item) => Session.Delete(item);

    public IEnumerable<WarehouseEntity> GetAllByProductionSite(ProductionSiteEntity productionSite) =>
        Session.Query<WarehouseEntity>()
            .Where(i => i.ProductionSite == productionSite)
            .OrderBy(i => i.Name).ToList();
}