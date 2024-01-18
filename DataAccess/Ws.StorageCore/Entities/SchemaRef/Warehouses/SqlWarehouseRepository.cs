namespace Ws.StorageCore.Entities.SchemaRef.Warehouses;

public sealed class SqlWarehouseRepository : SqlTableRepositoryBase<SqlWarehouseEntity>
{
    public IEnumerable<SqlWarehouseEntity> GetEnumerable()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlWarehouseEntity>(crud);
    }
}