using Ws.Database.Nhibernate.Entities.Ref1c.Clips;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Services.Features.Clips;

internal class ClipService(SqlClipRepository clipRepo) : IClipService
{
    #region List

    [Transactional]
    public IList<Clip> GetAll() => clipRepo.GetAll();

    #endregion

    #region Items

    [Transactional]
    public Clip GetItemByUid(Guid uid) => clipRepo.GetByUid(uid);

    #endregion
}