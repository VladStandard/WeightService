using Ws.Database.Core.Entities.Ref.ProductionSites;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Services.Features.ProductionSite;

internal class ProductionSiteService : IProductionSiteService
{
    public ProductionSiteEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<ProductionSiteEntity>(uid);

    public IEnumerable<ProductionSiteEntity> GetAll() => new SqlProductionSiteRepository().GetEnumerable();
}