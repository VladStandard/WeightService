using Ws.Domain.Models.Entities.Print;

namespace Ws.Domain.Services.Features.Pallets;

public interface IPalletService
{
    void Create(Pallet pallet, IList<Label> labels);
}