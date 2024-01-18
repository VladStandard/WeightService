namespace Ws.StorageCore.Entities.SchemaRef.StorageMethods;

public class SqlStorageMethodRepository : SqlTableRepositoryBase<SqlStorageMethodEntity>
{
    public IEnumerable<SqlStorageMethodEntity> GetList()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlStorageMethodEntity>(crud).ToList();
    }
}