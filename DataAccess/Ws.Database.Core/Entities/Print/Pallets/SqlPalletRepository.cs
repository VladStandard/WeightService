using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Print.Pallets;

public sealed class SqlPalletRepository : BaseRepository
{
    public IEnumerable<ViewPallet> GetAllViewByWarehouse(WarehouseEntity warehouse)
    {
        return SqlCoreHelper.Instance.GetEnumerable(
            QueryOver.Of<ViewPallet>().Where(i => i.Warehouse == warehouse.Name).OrderBy(i => i.CreateDt).Desc()
        );
    }
    
    public IEnumerable<LabelEntity> GetAllLabels(Guid palletUid)
    {
        return SqlCoreHelper.Instance.GetEnumerable(
            QueryOver.Of<LabelEntity>().Where(i => i.Pallet!.Uid == palletUid).OrderBy(i => i.CreateDt).Desc()
        );
    }
    
    public ViewPallet GetViewByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<ViewPallet>(uid);
}