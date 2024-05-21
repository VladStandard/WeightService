using Ws.Database.Nhibernate.Entities.Ref1c.Boxes;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Boxes;

internal class BoxService(SqlBoxRepository boxRepo) : IBoxService
{
    #region List

    [Transactional]
    public IList<Box> GetAll() => boxRepo.GetAll();

    #endregion

    #region Items

    [Transactional]
    public Box GetItemByUid(Guid uid) => boxRepo.GetByUid(uid);

    #endregion
}