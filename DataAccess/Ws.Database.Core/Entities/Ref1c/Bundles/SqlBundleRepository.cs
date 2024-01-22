using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Database.Core.Entities.Ref1c.Bundles;

public sealed class SqlBundleRepository : IUid1CRepo<BundleEntity>, IUidRepo<BundleEntity>
{
    public BundleEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<BundleEntity>(uid);
    
    public BundleEntity GetByUid1C(Guid uid1C)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCoreHelper.Instance.GetItemByCrud<BundleEntity>(sqlCrudConfig);
    }
    
    public IEnumerable<BundleEntity> GetEnumerable()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.Asc(nameof(BundleEntity.Weight)));
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCoreHelper.Instance.GetEnumerable<BundleEntity>(crud);
    }

}