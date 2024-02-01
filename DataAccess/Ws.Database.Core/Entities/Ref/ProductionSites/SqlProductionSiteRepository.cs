using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.ProductionSites;

public sealed class SqlProductionSiteRepository : IGetItemByUid<ProductionSiteEntity>, IGetAll<ProductionSiteEntity>
{
    public ProductionSiteEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<ProductionSiteEntity>(uid);
    
    public IEnumerable<ProductionSiteEntity> GetAll()
    {
        return SqlCoreHelper.Instance.GetEnumerable(
            QueryOver.Of<ProductionSiteEntity>().OrderBy(i => i.Name).Asc
        );
    }
}