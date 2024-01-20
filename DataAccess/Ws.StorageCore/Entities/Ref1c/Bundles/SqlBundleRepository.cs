using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.StorageCore.Entities.Ref1c.Bundles;

public sealed class SqlBundleRepository : SqlTableRepositoryBase<BundleEntity>
{
    public BundleEntity GetItemByUid1C(Guid uid1C)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCore.GetItemByCrud<BundleEntity>(sqlCrudConfig);
    }
    
    public IEnumerable<BundleEntity> GetEnumerable()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.Asc(nameof(BundleEntity.Weight)));
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<BundleEntity>(crud);
    }
}