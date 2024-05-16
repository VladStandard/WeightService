using Ws.Database.Nhibernate.Entities.Ref.PalletMen;
using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.PalletMen.Validators;

namespace Ws.Domain.Services.Features.PalletMen;

internal class PalletManService(SqlPalletManRepository palletManRepo) : IPalletManService
{
    [Transactional]
    public PalletMan GetItemByUid(Guid uid) => palletManRepo.GetByUid(uid);

    [Transactional]
    public IEnumerable<PalletMan> GetAll() => palletManRepo.GetAll();

    [Transactional, Validate<PalletManNewValidator>]
    public PalletMan Create(PalletMan item) => palletManRepo.Save(item);

    [Transactional, Validate<PalletManUpdateValidator>]
    public PalletMan Update(PalletMan item) => palletManRepo.Update(item);

    [Transactional]
    public void Delete(PalletMan item) => palletManRepo.Delete(item);
}