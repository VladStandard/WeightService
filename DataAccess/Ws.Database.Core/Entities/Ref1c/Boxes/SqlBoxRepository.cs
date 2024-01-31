using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Boxes;

public sealed class SqlBoxRepository : IGetItemByUid1C<BoxEntity>, IGetItemByUid<BoxEntity>, IGetAll<BoxEntity>
{
    public BoxEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<BoxEntity>(uid);
    
    public BoxEntity GetByUid1C(Guid uid1C)
    {
        return SqlCoreHelper.Instance.GetItem<BoxEntity>(
            DetachedCriteria.For<BoxEntity>()
                .Add(SqlRestrictions.EqualUid1C(uid1C))
        );
    }
    
    public IEnumerable<BoxEntity> GetAll()
    {
        return SqlCoreHelper.Instance.GetEnumerable<BoxEntity>(
            DetachedCriteria.For<BoxEntity>()
                .AddOrder(Order.Asc(nameof(BoxEntity.Weight)))
                .AddOrder(SqlOrder.NameAsc())
        );
    }
}