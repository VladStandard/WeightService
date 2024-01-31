using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Lines;

public sealed class SqlLineRepository : IGetItemByUid<LineEntity>, IGetItemByCriteria<LineEntity>
{
    public LineEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<LineEntity>(uid);

    public IEnumerable<LineEntity> GetAll()
    {
        return SqlCoreHelper.Instance.GetEnumerable<LineEntity>(
            DetachedCriteria.For<LineEntity>().AddOrder(SqlOrder.NameAsc())
        );
    }
    
    public LineEntity GetItemByCriteria(DetachedCriteria criteria) =>
        SqlCoreHelper.Instance.GetItem<LineEntity>(criteria);
}