using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.ProductionSites;

public interface IProductionSiteService : IGetItemByUid<ProductionSite>, IGetAll<ProductionSite>,
    ICreate<ProductionSite>, IUpdate<ProductionSite>, IDelete<ProductionSite>;