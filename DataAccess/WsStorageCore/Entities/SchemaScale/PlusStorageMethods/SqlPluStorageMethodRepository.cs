namespace WsStorageCore.Entities.SchemaScale.PlusStorageMethods;

public class SqlPluStorageMethodRepository : SqlTableRepositoryBase<SqlPluStorageMethodEntity>
{
    public List<SqlPluStorageMethodEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlPluStorageMethodEntity>(sqlCrudConfig).ToList();
    }
}