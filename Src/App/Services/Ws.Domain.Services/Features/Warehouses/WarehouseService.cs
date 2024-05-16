using Ws.Database.Nhibernate.Entities.Ref.Warehouses;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Warehouses.Validators;

namespace Ws.Domain.Services.Features.Warehouses;

internal class WarehouseService(SqlWarehouseRepository warehouseRepo) : IWarehouseService
{
    [Transactional]
    public Warehouse GetItemByUid(Guid uid) => warehouseRepo.GetByUid(uid);

    [Transactional]
    public IEnumerable<Warehouse> GetAll() => warehouseRepo.GetAll();

    [Transactional, Validate<WarehouseNewValidator>]
    public Warehouse Create(Warehouse item) => warehouseRepo.Save(item);

    [Transactional, Validate<WarehouseUpdateValidator>]
    public Warehouse Update(Warehouse item) => warehouseRepo.Update(item);

    [Transactional]
    public void Delete(Warehouse item) => warehouseRepo.Delete(item);

    [Transactional]
    public IEnumerable<Warehouse> GetAllByProductionSite(ProductionSite productionSite) =>
        warehouseRepo.GetAllByProductionSite(productionSite);
}