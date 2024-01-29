using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Bundles;

public sealed class SqlBundleRepository : IGetItemByUid1C<BundleEntity>, IGetItemByUid<BundleEntity>, IGetAll<BundleEntity>
{
    public BundleEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<BundleEntity>(uid);
    
    public BundleEntity GetByUid1C(Guid uid1C)
    {
        DetachedCriteria criteria = DetachedCriteria.For<BundleEntity>().Add(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCoreHelper.Instance.GetItemByCriteria<BundleEntity>(criteria);
    }
    
    public IEnumerable<BundleEntity> GetAll()
    {
        DetachedCriteria criteria = DetachedCriteria.For<BundleEntity>()
            .AddOrder(SqlOrder.Asc(nameof(BundleEntity.Weight)))
            .AddOrder(SqlOrder.NameAsc());
        return SqlCoreHelper.Instance.GetEnumerable<BundleEntity>(criteria);
    }

}