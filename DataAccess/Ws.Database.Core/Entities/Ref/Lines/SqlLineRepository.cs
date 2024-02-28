using Ws.Database.Core.Common.Commands;
using Ws.Database.Core.Common.Queries.Item;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Lines;

public sealed class SqlLineRepository : BaseRepository, 
    IGetItemByUid<LineEntity>, ISave<LineEntity>, IUpdate<LineEntity>, IDelete<LineEntity>
{
    public LineEntity GetByUid(Guid uid) => Session.Get<LineEntity>(uid) ?? new();
    public IEnumerable<LineEntity> GetAll() => Session.Query<LineEntity>().OrderBy(i => i.Name).ToList();

    public LineEntity GetByPcName(string pcName)
    {
        return Session.QueryOver<LineEntity>()
            .WhereRestrictionOn(i => i.PcName).IsInsensitiveLike(pcName, MatchMode.Exact)
            .SingleOrDefault() ?? new();
    }

    public LineEntity Save(LineEntity item) { Session.Save(item); return item; }    
    public LineEntity Update(LineEntity item) { Session.Update(item); return item; }
    public void Delete(LineEntity item) => Session.Delete(item);
}