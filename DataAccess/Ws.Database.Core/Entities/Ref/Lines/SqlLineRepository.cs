using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Lines;

public sealed class SqlLineRepository : IGetItemByUid<LineEntity>, IGetItemByCriteria<LineEntity>
{
    public LineEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<LineEntity>(uid);

    public IEnumerable<LineEntity> GetAll()
    {
        DetachedCriteria criteria = DetachedCriteria.For<LineEntity>()
            .AddOrder(SqlOrder.NameAsc());
        return SqlCoreHelper.Instance.GetEnumerable<LineEntity>(criteria);
    }
    
    public LineEntity GetItemByName(string name)
    {
        DetachedCriteria criteria = DetachedCriteria.For<LineEntity>()
            .Add(SqlRestrictions.Equal(nameof(LineEntity.Name), name));
        return SqlCoreHelper.Instance.GetItemByCriteria<LineEntity>(criteria);
    }

    public LineEntity GetItemByCriteria(DetachedCriteria criteria) =>
        SqlCoreHelper.Instance.GetItemByCriteria<LineEntity>(criteria);
}