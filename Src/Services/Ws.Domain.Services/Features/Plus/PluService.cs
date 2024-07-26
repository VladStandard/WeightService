using Ws.Database.Nhibernate.Common;
using Ws.Domain.Models.Entities.Ref1c.Plus;

namespace Ws.Domain.Services.Features.Plus;

public class PluService : BaseRepository
{
    [Transactional]
    public Plu GetItemByUid(Guid uid) => Session.Get<Plu>(uid) ?? new();
}