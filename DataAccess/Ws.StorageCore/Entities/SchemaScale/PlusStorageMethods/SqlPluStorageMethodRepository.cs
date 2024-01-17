namespace Ws.StorageCore.Entities.SchemaScale.PlusStorageMethods;

public class SqlPluStorageMethodRepository : SqlTableRepositoryBase<SqlPluStorageMethodEntity>
{
    public List<SqlPluStorageMethodEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlPluStorageMethodEntity>(sqlCrudConfig).ToList();
    }
}