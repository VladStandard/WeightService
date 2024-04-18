using Ws.Database.Nhibernate.Entities.Ref.Warehouses;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Warehouse.Validators;

namespace Ws.Domain.Services.Features.Warehouse;

internal class WarehouseService(SqlWarehouseRepository warehouseRepo) : IWarehouseService
{
    [Transactional]
    public WarehouseEntity GetItemByUid(Guid uid) => warehouseRepo.GetByUid(uid);

    [Transactional]
    public IEnumerable<WarehouseEntity> GetAll() => warehouseRepo.GetAll();

    [Transactional, Validate<WarehouseNewValidator>]
    public WarehouseEntity Create(WarehouseEntity item) => warehouseRepo.Save(item);

    [Transactional, Validate<WarehouseUpdateValidator>]
    public WarehouseEntity Update(WarehouseEntity item) => warehouseRepo.Update(item);

    [Transactional]
    public void Delete(WarehouseEntity item) => warehouseRepo.Delete(item);
}