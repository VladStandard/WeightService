using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref.PlusLines;

public sealed class SqlPluLineRepository : IGetListByCriteria<PluLineEntity>
{
    public PluLineEntity GetItemByLinePlu(LineEntity line, PluEntity plu)
    {
        DetachedCriteria criteria = DetachedCriteria.For<PluLineEntity>()
            .Add(Restrictions.Eq(nameof(PluLineEntity.Line), line))
            .Add(Restrictions.Eq(nameof(PluLineEntity.Plu), plu));
        return SqlCoreHelper.Instance.GetItemByCriteria<PluLineEntity>(criteria);
    }
    
    public IEnumerable<PluLineEntity> GetListByCriteria(DetachedCriteria criteria)
    {
        return SqlCoreHelper.Instance.GetEnumerable<PluLineEntity>(criteria).OrderBy(i => i.Plu.Number);
    }
}