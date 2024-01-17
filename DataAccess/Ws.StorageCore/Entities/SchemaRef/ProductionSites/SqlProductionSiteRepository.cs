namespace Ws.StorageCore.Entities.SchemaRef.ProductionSites;

public sealed class SqlProductionSiteRepository : SqlTableRepositoryBase<SqlProductionSiteEntity>
{
    public IEnumerable<SqlProductionSiteEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlProductionSiteEntity>(sqlCrudConfig);
    }
}