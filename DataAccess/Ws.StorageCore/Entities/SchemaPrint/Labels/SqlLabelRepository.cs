namespace Ws.StorageCore.Entities.SchemaPrint.Labels;

public sealed class SqlLabelRepository : SqlTableRepositoryBase<SqlLabelEntity>
{
    public IEnumerable<SqlLabelEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetEnumerable<SqlLabelEntity>(sqlCrudConfig).ToList();
    }
    
    public SqlLabelEntity GetItem(SqlCrudConfigModel sqlCrudConfig)
    {
        return SqlCore.GetItemByCrud<SqlLabelEntity>(sqlCrudConfig);
    }
}