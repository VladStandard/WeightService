using Ws.Database.Core.Common.Commands;
using Ws.Database.Core.Common.Queries.List;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.PlusLines;

public sealed class SqlPluLineRepository : BaseRepository, IGetListByQuery<PluLineEntity>, IDelete<PluLineEntity>,
    ISave<PluLineEntity>
{
    public IEnumerable<PluLineEntity> GetListByQuery(QueryOver<PluLineEntity> query) =>
        query.DetachedCriteria.GetExecutableCriteria(Session).List<PluLineEntity>().OrderBy(i => i.Plu.Number);

    public void Delete(PluLineEntity item) => Session.Delete(item);
    public PluLineEntity Save(PluLineEntity pluLine) { Session.Save(pluLine); return pluLine; }
}