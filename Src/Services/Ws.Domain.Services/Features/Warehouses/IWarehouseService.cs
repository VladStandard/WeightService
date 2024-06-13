using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Warehouses;

public interface IWarehouseService : IGetAll<Warehouse>, IGetItemByUid<Warehouse>,
    ICreate<Warehouse>, IUpdate<Warehouse>, IDelete<Warehouse>
{
    public IList<Warehouse> GetAllByProductionSite(ProductionSite productionSite);
}