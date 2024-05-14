using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Nhibernate.Entities.Ref1c.Clips;

public sealed class SqlClipRepository : BaseRepository, IGetItemByUid<Clip>, IGetAll<Clip>
{
    public Clip GetByUid(Guid uid) => Session.Get<Clip>(uid) ?? new();

    public IEnumerable<Clip> GetAll() =>
        Session.Query<Clip>().OrderBy(i => i.Weight).ThenBy(i => i.Name).ToList();
}