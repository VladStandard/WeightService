using Ws.Database.Core.Common.Commands;
using Ws.Database.Core.Common.Queries.Item;
using Ws.Domain.Models.Entities.Diag;

namespace Ws.Database.Core.Entities.Diag.LogWebs;

public class SqlLogWebRepository : BaseRepository, IGetItemByUid<LogWebEntity>, ISave<LogWebEntity>
{
    public LogWebEntity GetByUid(Guid uid) => Session.Get<LogWebEntity>(uid) ?? new();

    public IEnumerable<LogWebEntity> GetList() =>
        Session.Query<LogWebEntity>().OrderByDescending(i => i.CreateDt).Take(500).ToList();

    public LogWebEntity Save(LogWebEntity item) { Session.Save(item); return item; }}