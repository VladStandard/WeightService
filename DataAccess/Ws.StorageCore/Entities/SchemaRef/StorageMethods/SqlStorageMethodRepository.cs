namespace Ws.StorageCore.Entities.SchemaRef.StorageMethods;

public class SqlStorageMethodRepository : SqlTableRepositoryBase<SqlStorageMethodEntity>
{
    public IEnumerable<SqlStorageMethodEntity> GetList()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlStorageMethodEntity>(crud).ToList();
    }
    
    public SqlStorageMethodEntity GetItemByName(string name)
    {
        SqlCrudConfigModel crud = new();
        crud.AddFilter(SqlRestrictions.Equal(nameof(SqlStorageMethodEntity.Name), name));
        return SqlCore.GetItemByCrud<SqlStorageMethodEntity>(crud);
    }
}