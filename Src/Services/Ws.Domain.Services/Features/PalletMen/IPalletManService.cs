using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Users;

namespace Ws.Domain.Services.Features.PalletMen;

public interface IPalletManService : IGetItemByUid<PalletMan>,
    ICreate<PalletMan>, IUpdate<PalletMan>, IDelete<Guid>
{
    List<PalletMan> GetAllByProductionSite(ProductionSite site);
}