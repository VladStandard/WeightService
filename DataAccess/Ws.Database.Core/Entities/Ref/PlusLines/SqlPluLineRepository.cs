using Ws.Database.Core.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.PlusLines;

public sealed class SqlPluLineRepository : BaseRepository, IGetListByQuery<PluLineEntity>
{
    public IEnumerable<PluLineEntity> GetListByQuery(QueryOver<PluLineEntity> query) =>
        query.DetachedCriteria.GetExecutableCriteria(Session).List<PluLineEntity>().OrderBy(i => i.Plu.Number);
}