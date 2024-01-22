using Ws.Domain.Models.Entities.Ref;
using Ws.StorageCore.Entities.Ref.ProductionSites;
using Ws.StorageCore.Helpers;

namespace Ws.Services.Features.ProductionSite;

internal class ProductionSiteService : IProductionSiteService
{
    public ProductionSiteEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<ProductionSiteEntity>(uid);

    public IEnumerable<ProductionSiteEntity> GetAll() => new SqlProductionSiteRepository().GetEnumerable();
}