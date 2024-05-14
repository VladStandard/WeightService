using Ws.Database.Nhibernate.Entities.Ref.Warehouses;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Warehouse.Validators;

namespace Ws.Domain.Services.Features.Warehouse;

internal class WarehouseService(SqlWarehouseRepository warehouseRepo) : IWarehouseService
{
    [Transactional]
    public Models.Entities.Ref.Warehouse GetItemByUid(Guid uid) => warehouseRepo.GetByUid(uid);

    [Transactional]
    public IEnumerable<Models.Entities.Ref.Warehouse> GetAll() => warehouseRepo.GetAll();

    [Transactional, Validate<WarehouseNewValidator>]
    public Models.Entities.Ref.Warehouse Create(Models.Entities.Ref.Warehouse item) => warehouseRepo.Save(item);

    [Transactional, Validate<WarehouseUpdateValidator>]
    public Models.Entities.Ref.Warehouse Update(Models.Entities.Ref.Warehouse item) => warehouseRepo.Update(item);

    [Transactional]
    public void Delete(Models.Entities.Ref.Warehouse item) => warehouseRepo.Delete(item);

    [Transactional]
    public IEnumerable<Models.Entities.Ref.Warehouse> GetAllByProductionSite(Models.Entities.Ref.ProductionSite productionSite) =>
        warehouseRepo.GetAllByProductionSite(productionSite);
}