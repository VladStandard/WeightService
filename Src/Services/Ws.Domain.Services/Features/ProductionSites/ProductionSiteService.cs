using Ws.Database.Nhibernate.Entities.Ref.ProductionSites;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.ProductionSites.Validators;

namespace Ws.Domain.Services.Features.ProductionSites;

internal class ProductionSiteService(SqlProductionSiteRepository productionSiteRepo) : IProductionSiteService
{
    #region Items

    [Transactional]
    public ProductionSite GetItemByUid(Guid uid) => productionSiteRepo.GetByUid(uid);

    #endregion

    #region List

    [Transactional]
    public IList<ProductionSite> GetAll() => productionSiteRepo.GetAll();

    #endregion

    #region CRUD

    [Transactional, Validate<ProductionSiteNewValidator>]
    public ProductionSite Create(ProductionSite item) => productionSiteRepo.Save(item);

    [Transactional, Validate<ProductionSiteUpdateValidator>]
    public ProductionSite Update(ProductionSite item) => productionSiteRepo.Update(item);

    [Transactional]
    public void DeleteById(Guid id) => productionSiteRepo.Delete(new() { Uid = id });

    #endregion
}