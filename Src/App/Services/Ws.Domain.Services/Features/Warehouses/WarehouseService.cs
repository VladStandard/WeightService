using Ws.Database.Nhibernate.Entities.Ref.Warehouses;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Warehouses.Specs;
using Ws.Domain.Services.Features.Warehouses.Validators;

namespace Ws.Domain.Services.Features.Warehouses;

internal class WarehouseService(SqlWarehouseRepository warehouseRepo) : IWarehouseService
{
    #region List

    [Transactional]
    public IList<Warehouse> GetAll() => warehouseRepo.GetAll();

    [Transactional]
    public IList<Warehouse> GetAllByProductionSite(ProductionSite productionSite) =>
        warehouseRepo.GetAllBySpec(WarehouseSpecs.GetByProductionSite(productionSite));

    #endregion

    #region Items

    [Transactional]
    public Warehouse GetItemByUid(Guid uid) => warehouseRepo.GetByUid(uid);

    #endregion

    #region CRUD

    [Transactional, Validate<WarehouseNewValidator>]
    public Warehouse Create(Warehouse item) => warehouseRepo.Save(item);

    [Transactional, Validate<WarehouseUpdateValidator>]
    public Warehouse Update(Warehouse item) => warehouseRepo.Update(item);

    [Transactional]
    public void Delete(Warehouse item) => warehouseRepo.Delete(item);

    #endregion
}