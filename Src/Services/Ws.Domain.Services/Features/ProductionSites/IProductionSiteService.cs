using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.ProductionSites;

public interface IProductionSiteService : IGetItemByUid<ProductionSite>, IGetAll<ProductionSite>,
    ICreate<ProductionSite>, IUpdate<ProductionSite>, IDelete<Guid>;