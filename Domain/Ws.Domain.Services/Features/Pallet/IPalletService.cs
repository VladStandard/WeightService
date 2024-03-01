using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Common.Commands;

namespace Ws.Domain.Services.Features.Pallet;

public interface IPalletService : ICreate<PalletEntity>
{
    IEnumerable<ViewPallet> GetAllViewByWarehouse(WarehouseEntity warehouse);
    ViewPallet GetViewByUid(Guid uid);
    IEnumerable<LabelEntity> GetAllLabels(Guid palletUid);
}