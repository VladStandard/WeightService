namespace WsStorageCore.Entities.SchemaScale.Versions;

public class SqlVersionRepository : SqlTableRepositoryBase<SqlVersionEntity>
{
    public List<SqlVersionEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.Desc(nameof(SqlVersionEntity.Version)));
        return SqlCore.GetEnumerable<SqlVersionEntity>(sqlCrudConfig).ToList();
    }
}