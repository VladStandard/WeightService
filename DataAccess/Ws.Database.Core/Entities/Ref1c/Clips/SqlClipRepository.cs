using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Clips;

public sealed class SqlClipRepository : IGetItemByUid1C<ClipEntity>, IGetItemByUid<ClipEntity>, IGetAll<ClipEntity>
{
    public ClipEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<ClipEntity>(uid);
    
    public IEnumerable<ClipEntity> GetAll()
    {
        DetachedCriteria criteria = DetachedCriteria.For<ClipEntity>().AddOrder(SqlOrder.NameAsc());
        return SqlCoreHelper.Instance.GetEnumerable<ClipEntity>(criteria);
    }

    public ClipEntity GetByUid1C(Guid uid1C)
    {
        DetachedCriteria criteria = DetachedCriteria.For<ClipEntity>().Add(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCoreHelper.Instance.GetItemByCriteria<ClipEntity>(criteria);
    }
}