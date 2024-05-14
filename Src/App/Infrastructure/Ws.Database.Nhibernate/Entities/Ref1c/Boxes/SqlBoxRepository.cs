using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Nhibernate.Entities.Ref1c.Boxes;

public sealed class SqlBoxRepository : BaseRepository, IGetItemByUid<Box>, IGetAll<Box>
{
    public Box GetByUid(Guid uid) => Session.Get<Box>(uid) ?? new();

    public IEnumerable<Box> GetAll() =>
        Session.Query<Box>().OrderBy(i => i.Weight).ThenBy(i => i.Name).ToList();
}