using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Plus;

public sealed class SqlPluRepository : IGetItemByUid1C<PluEntity>, IGetItemByUid<PluEntity>, IGetAll<PluEntity>, 
    IGetListByCriteria<PluEntity>
{
    public PluEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<PluEntity>(uid);
    
    public PluEntity GetByUid1C(Guid uid1C)
    {
        DetachedCriteria criteria = DetachedCriteria.For<PluEntity>()
            .Add(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCoreHelper.Instance.GetItemByCriteria<PluEntity>(criteria);
    }
    
    public IEnumerable<PluEntity> GetAll()
    {
        DetachedCriteria criteria = DetachedCriteria.For<PluEntity>()
            .AddOrder(SqlOrder.Asc(nameof(PluEntity.Number)));
        return SqlCoreHelper.Instance.GetEnumerable<PluEntity>(criteria);
    }
    
    public IEnumerable<PluEntity> GetListByCriteria(DetachedCriteria criteria)
    {
        criteria.AddOrder(SqlOrder.Asc(nameof(PluEntity.Number)));
        return SqlCoreHelper.Instance.GetEnumerable<PluEntity>(criteria);
    }
}