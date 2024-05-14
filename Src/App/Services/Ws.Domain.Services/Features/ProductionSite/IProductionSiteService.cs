using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.ProductionSite;

public interface IProductionSiteService : IGetItemByUid<Models.Entities.Ref.ProductionSite>, IGetAll<Models.Entities.Ref.ProductionSite>,
    ICreate<Models.Entities.Ref.ProductionSite>, IUpdate<Models.Entities.Ref.ProductionSite>, IDelete<Models.Entities.Ref.ProductionSite>;