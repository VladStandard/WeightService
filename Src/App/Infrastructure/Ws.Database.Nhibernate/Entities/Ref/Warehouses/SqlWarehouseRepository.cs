using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Nhibernate.Entities.Ref.Warehouses;

public sealed class SqlWarehouseRepository : BaseRepository, IGetItemByUid<Warehouse>, IGetAll<Warehouse>,
    ISave<Warehouse>, IUpdate<Warehouse>, IDelete<Warehouse>
{
    public Warehouse GetByUid(Guid uid) => Session.Get<Warehouse>(uid) ?? new();
    public IEnumerable<Warehouse> GetAll() => Session.Query<Warehouse>().OrderBy(i => i.Name).ToList();
    public Warehouse Save(Warehouse item) { Session.Save(item); return item; }
    public Warehouse Update(Warehouse item) { Session.Update(item); return item; }
    public void Delete(Warehouse item) => Session.Delete(item);

    public IEnumerable<Warehouse> GetAllByProductionSite(ProductionSite productionSite) =>
        Session.Query<Warehouse>()
            .Where(i => i.ProductionSite == productionSite)
            .OrderBy(i => i.Name).ToList();
}