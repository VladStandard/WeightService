using Ws.Database.Nhibernate.Entities.Ref.ProductionSites;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.ProductionSite.Validators;

namespace Ws.Domain.Services.Features.ProductionSite;

internal class ProductionSiteService(SqlProductionSiteRepository productionSiteRepo) : IProductionSiteService
{
    [Transactional]
    public Models.Entities.Ref.ProductionSite GetItemByUid(Guid uid) => productionSiteRepo.GetByUid(uid);

    [Transactional]
    public IEnumerable<Models.Entities.Ref.ProductionSite> GetAll() => productionSiteRepo.GetAll();

    [Transactional, Validate<ProductionSiteNewValidator>]
    public Models.Entities.Ref.ProductionSite Create(Models.Entities.Ref.ProductionSite item) => productionSiteRepo.Save(item);

    [Transactional, Validate<ProductionSiteUpdateValidator>]
    public Models.Entities.Ref.ProductionSite Update(Models.Entities.Ref.ProductionSite item) => productionSiteRepo.Update(item);

    [Transactional]
    public void Delete(Models.Entities.Ref.ProductionSite item) => productionSiteRepo.Delete(item);
}