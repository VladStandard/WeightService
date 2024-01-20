using Ws.Domain.Models.Entities.Ref;

namespace Ws.StorageCore.Entities.Ref.ProductionSites;

public sealed class SqlProductionSiteRepository : SqlTableRepositoryBase<ProductionSiteEntity>
{
    public IEnumerable<ProductionSiteEntity> GetEnumerable()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<ProductionSiteEntity>(crud);
    }
}