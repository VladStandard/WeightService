using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.PlusLines;

public sealed class SqlPluLineRepository : IGetListByCriteria<PluLineEntity>, IGetItemByCriteria<PluLineEntity>
{
    public IEnumerable<PluLineEntity> GetListByCriteria(DetachedCriteria criteria)
    {
        return SqlCoreHelper.Instance.GetEnumerable<PluLineEntity>(criteria).OrderBy(i => i.Plu.Number);
    }

    public PluLineEntity GetItemByCriteria(DetachedCriteria criteria) => 
        SqlCoreHelper.Instance.GetItem<PluLineEntity>(criteria);
}