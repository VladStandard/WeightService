using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.StorageCore.Entities.Ref1c.Brands;

public sealed class SqlBrandRepository : SqlTableRepositoryBase<BrandEntity>
{
    public IEnumerable<BrandEntity> GetEnumerable()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<BrandEntity>(crud);
    }

    public BrandEntity GetItemByUid1C(Guid uid1C)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCore.GetItemByCrud<BrandEntity>(sqlCrudConfig);
    }
}