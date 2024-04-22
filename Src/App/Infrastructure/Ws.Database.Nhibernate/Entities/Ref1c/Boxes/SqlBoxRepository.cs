using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Nhibernate.Entities.Ref1c.Boxes;

public sealed class SqlBoxRepository : BaseRepository, IGetItemByUid<BoxEntity>, IGetAll<BoxEntity>
{
    public BoxEntity GetByUid(Guid uid) => Session.Get<BoxEntity>(uid) ?? new();

    public IEnumerable<BoxEntity> GetAll() =>
        Session.Query<BoxEntity>().OrderBy(i => i.Weight).ThenBy(i => i.Name).ToList();
}