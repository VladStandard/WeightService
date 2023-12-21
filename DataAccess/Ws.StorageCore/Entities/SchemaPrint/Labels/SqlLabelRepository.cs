namespace Ws.StorageCore.Entities.SchemaPrint.Labels;

public sealed class SqlLabelRepository : SqlTableRepositoryBase<SqlLabelEntity>
{
    public List<SqlLabelEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetEnumerable<SqlLabelEntity>(sqlCrudConfig).ToList();
    }
}