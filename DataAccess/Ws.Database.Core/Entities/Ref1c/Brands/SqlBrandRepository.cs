using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Brands;

public sealed class SqlBrandRepository : IUid1CRepo<BrandEntity>, IUidRepo<BrandEntity>
{
    public BrandEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<BrandEntity>(uid);
    
    public BrandEntity GetByUid1C(Guid uid1C)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCoreHelper.Instance.GetItemByCrud<BrandEntity>(sqlCrudConfig);
    }
    
    public IEnumerable<BrandEntity> GetEnumerable()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCoreHelper.Instance.GetEnumerable<BrandEntity>(crud);
    }
}