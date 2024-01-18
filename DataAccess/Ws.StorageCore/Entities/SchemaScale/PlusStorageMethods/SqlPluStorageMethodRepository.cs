namespace Ws.StorageCore.Entities.SchemaScale.PlusStorageMethods;

public class SqlPluStorageMethodRepository : SqlTableRepositoryBase<SqlPluStorageMethodEntity>
{
    public IEnumerable<SqlPluStorageMethodEntity> GetList()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlPluStorageMethodEntity>(crud).ToList();
    }
}