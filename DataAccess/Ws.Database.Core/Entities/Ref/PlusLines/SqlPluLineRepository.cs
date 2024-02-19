using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.PlusLines;

public sealed class SqlPluLineRepository : BaseRepository, IGetItemByQuery<PluLineEntity>, IGetListByQuery<PluLineEntity>
{
    public PluLineEntity GetItemByQuery(QueryOver<PluLineEntity> query) =>
        query.DetachedCriteria.GetExecutableCriteria(Session).UniqueResult<PluLineEntity>();
    
    public IEnumerable<PluLineEntity> GetListByQuery(QueryOver<PluLineEntity> query) =>
        query.DetachedCriteria.GetExecutableCriteria(Session).List<PluLineEntity>().OrderBy(i => i.Plu.Number);
}