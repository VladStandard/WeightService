using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Brands;

public sealed class SqlBrandRepository : IGetItemByUid1C<BrandEntity>, IGetItemByUid<BrandEntity>, IGetAll<BrandEntity>
{
    public BrandEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<BrandEntity>(uid);
    
    public BrandEntity GetByUid1C(Guid uid1C)
    {
        DetachedCriteria criteria = DetachedCriteria.For<BrandEntity>().Add(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCoreHelper.Instance.GetItemByCriteria<BrandEntity>(criteria);
    }
    
    public IEnumerable<BrandEntity> GetAll()
    {
        DetachedCriteria criteria = DetachedCriteria.For<BrandEntity>().AddOrder(SqlOrder.NameAsc());
        return SqlCoreHelper.Instance.GetEnumerable<BrandEntity>(criteria);
    }
}