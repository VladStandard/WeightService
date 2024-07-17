using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.Warehouses;

public interface IWarehouseService : IGetItemByUid<Warehouse>,
    ICreate<Warehouse>, IUpdate<Warehouse>, IDelete<Guid>
{
    public IList<Warehouse> GetAllByProductionSite(ProductionSite productionSite);
}