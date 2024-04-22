using Ws.Database.Nhibernate.Entities.Ref.ProductionSites;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.ProductionSite.Validators;

namespace Ws.Domain.Services.Features.ProductionSite;

internal class ProductionSiteService(SqlProductionSiteRepository productionSiteRepo) : IProductionSiteService
{
    [Transactional]
    public ProductionSiteEntity GetItemByUid(Guid uid) => productionSiteRepo.GetByUid(uid);

    [Transactional]
    public IEnumerable<ProductionSiteEntity> GetAll() => productionSiteRepo.GetAll();

    [Transactional, Validate<ProductionSiteNewValidator>]
    public ProductionSiteEntity Create(ProductionSiteEntity item) => productionSiteRepo.Save(item);

    [Transactional, Validate<ProductionSiteUpdateValidator>]
    public ProductionSiteEntity Update(ProductionSiteEntity item) => productionSiteRepo.Update(item);

    [Transactional]
    public void Delete(ProductionSiteEntity item) => productionSiteRepo.Delete(item);
}