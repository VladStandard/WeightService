using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Lines;

public sealed class SqlLineRepository : BaseRepository, IGetItemByUid<LineEntity>, IGetItemByQuery<LineEntity>
{
    public LineEntity GetByUid(Guid uid) => Session.Get<LineEntity>(uid) ?? new();

    public IEnumerable<LineEntity> GetAll() =>
        Session.QueryOver<LineEntity>().OrderBy(i => i.Name).Asc.List();
    
    public LineEntity GetItemByQuery(QueryOver<LineEntity> query) => 
        query.DetachedCriteria.GetExecutableCriteria(Session).UniqueResult<LineEntity>();
}