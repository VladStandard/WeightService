using Ws.Database.Nhibernate.Entities.Ref.PalletMen;
using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.PalletMen.Validators;

namespace Ws.Domain.Services.Features.PalletMen;

internal class PalletManService(SqlPalletManRepository palletManRepo) : IPalletManService
{
    #region Items

    [Transactional]
    public PalletMan GetItemByUid(Guid uid) => palletManRepo.GetByUid(uid);

    #endregion

    #region List

    [Transactional]
    public IList<PalletMan> GetAll() => palletManRepo.GetAll();

    #endregion

    #region CRUD

    [Transactional, Validate<PalletManNewValidator>]
    public PalletMan Create(PalletMan item) => palletManRepo.Save(item);

    [Transactional, Validate<PalletManUpdateValidator>]
    public PalletMan Update(PalletMan item) => palletManRepo.Update(item);

    [Transactional]
    public void Delete(PalletMan item) => palletManRepo.Delete(item);

    #endregion
}