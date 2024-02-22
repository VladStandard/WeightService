using Ws.Database.Core.Entities.Ref.ProductionSites;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.ProductionSite;

internal class ProductionSiteService(SqlProductionSiteRepository productionSiteRepo) : IProductionSiteService
{
    [Transactional] public ProductionSiteEntity GetItemByUid(Guid uid) => productionSiteRepo.GetByUid(uid);
    [Transactional] public IEnumerable<ProductionSiteEntity> GetAll() => productionSiteRepo.GetAll();
}