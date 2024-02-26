using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Lines;

public sealed class SqlLineRepository : BaseRepository, IGetItemByUid<LineEntity>
{
    public LineEntity GetByUid(Guid uid) => Session.Get<LineEntity>(uid) ?? new();

    public IEnumerable<LineEntity> GetAll() =>
        Session.Query<LineEntity>().OrderBy(i => i.Name).ToList();
    
    public LineEntity GetByPcName(string pcName) =>
        Session.QueryOver<LineEntity>()
            .WhereRestrictionOn(i => i.PcName).IsInsensitiveLike(pcName, MatchMode.Exact)
            .SingleOrDefault() ?? new();
}