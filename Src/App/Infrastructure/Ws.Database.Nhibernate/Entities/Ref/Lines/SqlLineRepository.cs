using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Nhibernate.Entities.Ref.Lines;

public sealed class SqlLineRepository : BaseRepository,
    IGetItemByUid<Arm>, ISave<Arm>, IUpdate<Arm>, IDelete<Arm>
{
    public Arm GetByUid(Guid uid) => Session.Get<Arm>(uid) ?? new();
    public IEnumerable<Arm> GetAll() => Session.Query<Arm>().OrderBy(i => i.Name).ToList();
    public IEnumerable<Arm> GetAllByProductionSite(ProductionSite site)
    {
        return Session.Query<Arm>().Where(i => i.Warehouse.ProductionSite == site)
            .OrderBy(i => i.Name).ToList();
    }

    public Arm GetByPcName(string pcName)
    {
        return Session.QueryOver<Arm>()
            .WhereRestrictionOn(i => i.PcName).IsInsensitiveLike(pcName, MatchMode.Exact)
            .SingleOrDefault() ?? new();
    }

    public Arm Save(Arm item) { Session.Save(item); return item; }
    public Arm Update(Arm item) { Session.Update(item); return item; }
    public void Delete(Arm item) => Session.Delete(item);
}