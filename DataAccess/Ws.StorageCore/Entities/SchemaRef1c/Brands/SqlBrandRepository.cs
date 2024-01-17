namespace Ws.StorageCore.Entities.SchemaRef1c.Brands;

public sealed class SqlBrandRepository : SqlTableRepositoryBase<SqlBrandEntity>
{
    public IEnumerable<SqlBrandEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlBrandEntity>(sqlCrudConfig);
    }

    public SqlBrandEntity GetItemByUid1C(Guid uid1C)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCore.GetItemByCrud<SqlBrandEntity>(sqlCrudConfig);
    }
}