using Ws.Database.Core.Entities.Ref.ProductionSites;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.ProductionSite;

internal class ProductionSiteService : IProductionSiteService
{
    public ProductionSiteEntity GetByUid(Guid uid) =>  new SqlProductionSiteRepository().GetByUid(uid);

    public IEnumerable<ProductionSiteEntity> GetAll() => new SqlProductionSiteRepository().GetEnumerable();
}