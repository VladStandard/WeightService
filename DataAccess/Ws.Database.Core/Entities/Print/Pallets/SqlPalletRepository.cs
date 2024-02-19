using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Print.Pallets;

public sealed class SqlPalletRepository : BaseRepository
{
    public IEnumerable<ViewPallet> GetAllViewByWarehouse(WarehouseEntity warehouse) =>
        Session.Query<ViewPallet>().Where(i => i.Warehouse == warehouse.Name)
            .OrderByDescending(i => i.CreateDt).ToList();

    public IEnumerable<LabelEntity> GetAllLabels(Guid palletUid) =>
        Session.Query<LabelEntity>().Where(i => i.Pallet!.Uid == palletUid)
            .OrderByDescending(i => i.CreateDt).ToList();

    public ViewPallet GetViewByUid(Guid uid) => Session.Get<ViewPallet>(uid) ?? new();
}