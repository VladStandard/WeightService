using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Nhibernate.Entities.Ref1c.Clips;

public sealed class SqlClipRepository : BaseRepository, IGetItemByUid<ClipEntity>, IGetAll<ClipEntity>
{
    public ClipEntity GetByUid(Guid uid) => Session.Get<ClipEntity>(uid) ?? new();

    public IEnumerable<ClipEntity> GetAll() =>
        Session.Query<ClipEntity>().OrderBy(i => i.Weight).ThenBy(i => i.Name).ToList();
}