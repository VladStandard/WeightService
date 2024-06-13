using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Nhibernate.Entities.Print.Pallets;

public sealed class SqlPalletRepository : BaseRepository, ISave<Pallet>
{
    public IEnumerable<ViewPallet> GetAllViewByWarehouse(Warehouse warehouse) =>
        Session.Query<ViewPallet>().Where(i => i.Warehouse == warehouse.Name)
            .OrderByDescending(i => i.CreateDt).ToList();

    public IEnumerable<Label> GetAllLabels(Guid palletUid) =>
        Session.Query<Label>().Where(i => i.PalletUid == palletUid)
            .OrderByDescending(i => i.CreateDt).ToList();

    public ViewPallet GetViewByUid(Guid uid) => Session.Get<ViewPallet>(uid) ?? new();

    public Pallet Save(Pallet item) { Session.Save(item); return item; }
}