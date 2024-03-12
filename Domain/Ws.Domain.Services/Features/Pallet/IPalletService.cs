using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.Pallet;

public interface IPalletService
{
    IEnumerable<ViewPallet> GetAllViewByWarehouse(WarehouseEntity warehouse);
    ViewPallet GetViewByUid(Guid uid);
    IEnumerable<LabelEntity> GetAllLabels(Guid palletUid);
    void Create(PalletEntity pallet, IList<LabelEntity> labels);
}