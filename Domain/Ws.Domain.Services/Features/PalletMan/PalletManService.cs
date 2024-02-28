using Ws.Database.Core.Entities.Ref.PalletMen;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.PalletMan.Validators;

namespace Ws.Domain.Services.Features.PalletMan;

internal class PalletManService(SqlPalletManRepository palletManRepo) : IPalletManService
{
    [Transactional]
    public PalletManEntity GetItemByUid(Guid uid) => palletManRepo.GetByUid(uid);
    
    [Transactional]
    public IEnumerable<PalletManEntity> GetAll() => palletManRepo.GetAll();
    
    [Transactional, Validate<PalletManNewValidator>]
    public PalletManEntity Create(PalletManEntity item) => palletManRepo.Save(item);
    
    [Transactional, Validate<PalletManUpdateValidator>]
    public PalletManEntity Update(PalletManEntity item) => palletManRepo.Update(item);
}