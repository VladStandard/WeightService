using Ws.Database.Core.Entities.Print.Pallets;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.Pallet;

public class PalletService : IPalletService
{
    public IEnumerable<ViewPallet> GetAllViewByWarehouse(WarehouseEntity warehouse) =>
        new SqlPalletRepository().GetAllViewByWarehouse(warehouse);
    
    public ViewPallet GetViewByUid(Guid uid) => new SqlPalletRepository().GetViewByUid(uid);
    
    public IEnumerable<LabelEntity> GetAllLabels(Guid palletUid) => new SqlPalletRepository().GetAllLabels(palletUid);
}