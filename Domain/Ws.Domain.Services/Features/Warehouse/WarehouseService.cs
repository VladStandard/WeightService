using Ws.Database.Core.Entities.Ref.Warehouses;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Warehouse;

internal class WarehouseService(SqlWarehouseRepository warehouseRepo) : IWarehouseService
{
    [Session] public WarehouseEntity GetItemByUid(Guid uid) => warehouseRepo.GetByUid(uid);
    [Session] public IEnumerable<WarehouseEntity> GetAll() => warehouseRepo.GetAll();
}