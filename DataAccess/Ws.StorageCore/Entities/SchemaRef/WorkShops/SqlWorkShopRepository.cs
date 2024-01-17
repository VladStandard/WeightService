namespace Ws.StorageCore.Entities.SchemaRef.WorkShops;

public sealed class SqlWorkShopRepository : SqlTableRepositoryBase<SqlWorkShopEntity>
{
    public IEnumerable<SqlWorkShopEntity> GetEnumerable()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlWorkShopEntity>(crud);
    }
}