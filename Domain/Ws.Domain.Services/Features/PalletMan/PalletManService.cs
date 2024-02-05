using Ws.Database.Core.Entities.Ref.PalletMen;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.PalletMan;

public class PalletManService : IPalletManService
{
    public PalletManEntity GetItemByUid(Guid uid) => new SqlPalletManRepository().GetByUid(uid);
    public IEnumerable<PalletManEntity> GetAll() => new SqlPalletManRepository().GetAll();
}