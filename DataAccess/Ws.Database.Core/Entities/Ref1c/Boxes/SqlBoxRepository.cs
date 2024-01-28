using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Boxes;

public sealed class SqlBoxRepository : IGetItemByUid1C<BoxEntity>, IGetItemByUid<BoxEntity>, IGetAll<BoxEntity>
{
    public BoxEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<BoxEntity>(uid);
    
    public BoxEntity GetByUid1C(Guid uid1C)
    {
        DetachedCriteria criteria = DetachedCriteria.For<BoxEntity>()
            .Add(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCoreHelper.Instance.GetItemByCriteria<BoxEntity>(criteria);
    }
    
    public IEnumerable<BoxEntity> GetAll()
    {
        DetachedCriteria criteria = DetachedCriteria.For<BoxEntity>()
            .AddOrder(SqlOrder.Asc(nameof(BoxEntity.Weight)))
            .AddOrder(SqlOrder.NameAsc());
        return SqlCoreHelper.Instance.GetEnumerable<BoxEntity>(criteria);
    }
}