using Ws.Database.Core.Entities.Ref.Warehouses;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Warehouse;

internal class WarehouseService(SqlWarehouseRepository warehouseRepo) : IWarehouseService
{
    [Transactional] public WarehouseEntity GetItemByUid(Guid uid) => warehouseRepo.GetByUid(uid);
    [Transactional] public IEnumerable<WarehouseEntity> GetAll() => warehouseRepo.GetAll();
    [Transactional] public WarehouseEntity Create(WarehouseEntity item) => warehouseRepo.Save(item);
    [Transactional] public WarehouseEntity Update(WarehouseEntity item) => warehouseRepo.Update(item);
}