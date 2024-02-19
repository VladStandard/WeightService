using Ws.Database.Core.Entities.Print.Pallets;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Pallet;

public class PalletService(SqlPalletRepository palletRepo) : IPalletService
{
    [Session] public IEnumerable<ViewPallet> GetAllViewByWarehouse(WarehouseEntity warehouse) =>
        palletRepo.GetAllViewByWarehouse(warehouse);
    
    [Session] public ViewPallet GetViewByUid(Guid uid) => palletRepo.GetViewByUid(uid);
    
    [Session] public IEnumerable<LabelEntity> GetAllLabels(Guid palletUid) => palletRepo.GetAllLabels(palletUid);
}