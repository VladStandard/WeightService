using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.ProductionSites;

public sealed class SqlProductionSiteRepository :  BaseRepository, IGetItemByUid<ProductionSiteEntity>, IGetAll<ProductionSiteEntity>
{
    public ProductionSiteEntity GetByUid(Guid uid) => SqlCoreHelper.GetItemById<ProductionSiteEntity>(uid);
    
    public IEnumerable<ProductionSiteEntity> GetAll()
    {
        return SqlCoreHelper.GetEnumerable(
            QueryOver.Of<ProductionSiteEntity>().OrderBy(i => i.Name).Asc
        );
    }
}