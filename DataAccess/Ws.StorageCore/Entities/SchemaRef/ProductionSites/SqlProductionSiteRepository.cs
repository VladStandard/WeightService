namespace Ws.StorageCore.Entities.SchemaRef.ProductionSites;

public sealed class SqlProductionSiteRepository : SqlTableRepositoryBase<SqlProductionSiteEntity>
{
    public IEnumerable<SqlProductionSiteEntity> GetEnumerable()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlProductionSiteEntity>(crud);
    }
}