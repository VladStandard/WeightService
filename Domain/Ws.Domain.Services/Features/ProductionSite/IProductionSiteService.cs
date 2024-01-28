using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.ProductionSite;

public interface IProductionSiteService : IGetItemByUid<ProductionSiteEntity>, IGetAll<ProductionSiteEntity>;