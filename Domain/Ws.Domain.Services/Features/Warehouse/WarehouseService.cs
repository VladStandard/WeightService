using Ws.Database.Core.Entities.Ref.Warehouses;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.Warehouse;

internal class WarehouseService : IWarehouseService
{
    public WarehouseEntity GetItemByUid(Guid uid) => new SqlWarehouseRepository().GetByUid(uid);
    public IEnumerable<WarehouseEntity> GetAll() => new SqlWarehouseRepository().GetAll();
}