using Ws.Database.Core.Entities.Ref.PalletMen;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.PalletMan;

public class PalletManService(SqlPalletManRepository palletManRepo) : IPalletManService
{
    public PalletManEntity GetItemByUid(Guid uid) => palletManRepo.GetByUid(uid);
    public IEnumerable<PalletManEntity> GetAll() => palletManRepo.GetAll();
}