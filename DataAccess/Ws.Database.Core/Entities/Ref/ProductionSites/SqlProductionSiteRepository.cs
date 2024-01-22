using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.ProductionSites;

public sealed class SqlProductionSiteRepository : IUidRepo<ProductionSiteEntity>
{
    public ProductionSiteEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<ProductionSiteEntity>(uid);
    
    public IEnumerable<ProductionSiteEntity> GetEnumerable()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCoreHelper.Instance.GetEnumerable<ProductionSiteEntity>(crud);
    }
}