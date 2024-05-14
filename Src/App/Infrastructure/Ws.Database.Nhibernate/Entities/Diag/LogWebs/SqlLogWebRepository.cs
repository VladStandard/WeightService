using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Domain.Models.Entities.Diag;

namespace Ws.Database.Nhibernate.Entities.Diag.LogWebs;

public class SqlLogWebRepository : BaseRepository, IGetItemByUid<LogWeb>, ISave<LogWeb>
{
    public LogWeb GetByUid(Guid uid) => Session.Get<LogWeb>(uid) ?? new();

    public IEnumerable<LogWeb> GetList() =>
        Session.Query<LogWeb>().OrderByDescending(i => i.CreateDt).Take(500).ToList();

    public LogWeb Save(LogWeb item) { Session.Save(item); return item; }
}