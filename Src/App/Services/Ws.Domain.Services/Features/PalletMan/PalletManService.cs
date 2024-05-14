using Ws.Database.Nhibernate.Entities.Ref.PalletMen;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.PalletMan.Validators;

namespace Ws.Domain.Services.Features.PalletMan;

internal class PalletManService(SqlPalletManRepository palletManRepo) : IPalletManService
{
    [Transactional]
    public Models.Entities.Users.PalletMan GetItemByUid(Guid uid) => palletManRepo.GetByUid(uid);

    [Transactional]
    public IEnumerable<Models.Entities.Users.PalletMan> GetAll() => palletManRepo.GetAll();

    [Transactional, Validate<PalletManNewValidator>]
    public Models.Entities.Users.PalletMan Create(Models.Entities.Users.PalletMan item) => palletManRepo.Save(item);

    [Transactional, Validate<PalletManUpdateValidator>]
    public Models.Entities.Users.PalletMan Update(Models.Entities.Users.PalletMan item) => palletManRepo.Update(item);

    [Transactional]
    public void Delete(Models.Entities.Users.PalletMan item) => palletManRepo.Delete(item);
}