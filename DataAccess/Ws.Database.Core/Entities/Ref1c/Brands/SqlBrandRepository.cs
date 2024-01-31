using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Brands;

public sealed class SqlBrandRepository : IGetItemByUid1C<BrandEntity>, IGetItemByUid<BrandEntity>, IGetAll<BrandEntity>
{
    public BrandEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<BrandEntity>(uid);
    
    public BrandEntity GetByUid1C(Guid uid1C)
    {
        return SqlCoreHelper.Instance.GetItem<BrandEntity>(
            DetachedCriteria.For<BrandEntity>().Add(SqlRestrictions.EqualUid1C(uid1C))
        );
    }
    
    public IEnumerable<BrandEntity> GetAll()
    {
        return SqlCoreHelper.Instance.GetEnumerable<BrandEntity>(
            DetachedCriteria.For<BrandEntity>().AddOrder(SqlOrder.NameAsc())
        );
    }
}