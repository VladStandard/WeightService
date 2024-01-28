using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.ProductionSites;

public sealed class SqlProductionSiteRepository : IGetItemByUid<ProductionSiteEntity>, IGetAll<ProductionSiteEntity>
{
    public ProductionSiteEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<ProductionSiteEntity>(uid);
    
    public IEnumerable<ProductionSiteEntity> GetAll()
    {
        DetachedCriteria criteria = DetachedCriteria.For<ProductionSiteEntity>().AddOrder(SqlOrder.NameAsc());
        return SqlCoreHelper.Instance.GetEnumerable<ProductionSiteEntity>(criteria);
    }
}