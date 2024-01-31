using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Plus;

public sealed class SqlPluRepository : IGetItemByUid1C<PluEntity>, IGetItemByUid<PluEntity>, IGetAll<PluEntity>, 
    IGetListByCriteria<PluEntity>
{
    public PluEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<PluEntity>(uid);
    
    public PluEntity GetByUid1C(Guid uid1C)
    {
        return SqlCoreHelper.Instance.GetItem<PluEntity>(
            DetachedCriteria.For<PluEntity>().Add(SqlRestrictions.EqualUid1C(uid1C))
        );
    }
    
    public IEnumerable<PluEntity> GetAll()
    {
        return SqlCoreHelper.Instance.GetEnumerable<PluEntity>(
            DetachedCriteria.For<PluEntity>().AddOrder(Order.Asc(nameof(PluEntity.Number)))
        );
    }
    
    public IEnumerable<PluEntity> GetListByCriteria(DetachedCriteria criteria)
    {
        criteria.AddOrder(Order.Asc(nameof(PluEntity.Number)));
        return SqlCoreHelper.Instance.GetEnumerable<PluEntity>(criteria);
    }
}