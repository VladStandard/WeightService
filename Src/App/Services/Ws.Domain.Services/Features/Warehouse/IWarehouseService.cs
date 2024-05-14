using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Warehouse;

public interface IWarehouseService : IGetAll<Models.Entities.Ref.Warehouse>, IGetItemByUid<Models.Entities.Ref.Warehouse>,
    ICreate<Models.Entities.Ref.Warehouse>, IUpdate<Models.Entities.Ref.Warehouse>, IDelete<Models.Entities.Ref.Warehouse>
{
    public IEnumerable<Models.Entities.Ref.Warehouse> GetAllByProductionSite(Models.Entities.Ref.ProductionSite productionSite);
}