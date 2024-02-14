using Ws.Database.Core.Entities.Print.Pallets;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.Pallet;

public class PalletService(SqlPalletRepository palletRepo) : IPalletService
{
    public IEnumerable<ViewPallet> GetAllViewByWarehouse(WarehouseEntity warehouse) =>
        palletRepo.GetAllViewByWarehouse(warehouse);
    
    public ViewPallet GetViewByUid(Guid uid) => palletRepo.GetViewByUid(uid);
    
    public IEnumerable<LabelEntity> GetAllLabels(Guid palletUid) => palletRepo.GetAllLabels(palletUid);
}