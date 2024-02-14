using Ws.Database.Core.Entities.Ref.Warehouses;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.Warehouse;

internal class WarehouseService(SqlWarehouseRepository warehouseRepo) : IWarehouseService
{
    public WarehouseEntity GetItemByUid(Guid uid) => warehouseRepo.GetByUid(uid);
    public IEnumerable<WarehouseEntity> GetAll() => warehouseRepo.GetAll();
}