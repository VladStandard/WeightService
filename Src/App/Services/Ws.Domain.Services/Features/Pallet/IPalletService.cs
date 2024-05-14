using Ws.Domain.Models.Entities.Print;

namespace Ws.Domain.Services.Features.Pallet;

public interface IPalletService
{
    IEnumerable<ViewPallet> GetAllViewByWarehouse(Models.Entities.Ref.Warehouse warehouse);
    ViewPallet GetViewByUid(Guid uid);
    IEnumerable<Models.Entities.Print.Label> GetAllLabels(Guid palletUid);
    void Create(Models.Entities.Print.Pallet pallet, IList<Models.Entities.Print.Label> labels);
}