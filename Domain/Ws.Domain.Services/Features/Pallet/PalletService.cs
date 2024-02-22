using Ws.Database.Core.Entities.Print.Pallets;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Pallet;

public class PalletService(SqlPalletRepository palletRepo) : IPalletService
{
    [Transactional] public IEnumerable<ViewPallet> GetAllViewByWarehouse(WarehouseEntity warehouse) =>
        palletRepo.GetAllViewByWarehouse(warehouse);
    
    [Transactional] public ViewPallet GetViewByUid(Guid uid) => palletRepo.GetViewByUid(uid);
    
    [Transactional] public IEnumerable<LabelEntity> GetAllLabels(Guid palletUid) => palletRepo.GetAllLabels(palletUid);
}