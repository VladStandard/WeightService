using Ws.Database.Core.Entities.Ref.ProductionSites;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.ProductionSite;

internal class ProductionSiteService(SqlProductionSiteRepository productionSiteRepo) : IProductionSiteService
{
    public ProductionSiteEntity GetItemByUid(Guid uid) => productionSiteRepo.GetByUid(uid);

    public IEnumerable<ProductionSiteEntity> GetAll() => productionSiteRepo.GetAll();
}