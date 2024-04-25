using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Warehouse;

public interface IWarehouseService : IGetAll<WarehouseEntity>, IGetItemByUid<WarehouseEntity>,
    ICreate<WarehouseEntity>, IUpdate<WarehouseEntity>, IDelete<WarehouseEntity>
{
    public IEnumerable<WarehouseEntity> GetAllByProductionSite(ProductionSiteEntity productionSite);
}