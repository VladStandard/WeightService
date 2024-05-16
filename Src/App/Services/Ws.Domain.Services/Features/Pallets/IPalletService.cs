using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.Pallets;

public interface IPalletService
{
    IEnumerable<ViewPallet> GetAllViewByWarehouse(Warehouse warehouse);
    ViewPallet GetViewByUid(Guid uid);
    IEnumerable<Label> GetAllLabels(Guid palletUid);
    void Create(Pallet pallet, IList<Label> labels);
}