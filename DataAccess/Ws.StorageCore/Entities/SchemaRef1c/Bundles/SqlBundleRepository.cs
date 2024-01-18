namespace Ws.StorageCore.Entities.SchemaRef1c.Bundles;

public sealed class SqlBundleRepository : SqlTableRepositoryBase<SqlBundleEntity>
{
    public SqlBundleEntity GetItemByUid1C(Guid uid1C)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCore.GetItemByCrud<SqlBundleEntity>(sqlCrudConfig);
    }
    
    public IEnumerable<SqlBundleEntity> GetEnumerable()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.Asc(nameof(SqlBundleEntity.Weight)));
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlBundleEntity>(crud);
    }
}