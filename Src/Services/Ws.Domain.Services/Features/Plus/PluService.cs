using Ws.Database.Nhibernate.Entities.Ref1c.Plus;
using Ws.Domain.Models.Entities.Ref1c.Plus;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Plus;

internal class PluService(SqlPluRepository pluRepo) : IPluService
{
    #region Items

    [Transactional]
    public Plu GetItemByUid(Guid uid) => pluRepo.GetByUid(uid);

    #endregion

    #region List

    [Transactional]
    public IList<Plu> GetAll() => pluRepo.GetAll();

    #endregion

    #region CRUD

    [Transactional]
    public Plu Update(Plu item) => pluRepo.Update(item);

    #endregion
}