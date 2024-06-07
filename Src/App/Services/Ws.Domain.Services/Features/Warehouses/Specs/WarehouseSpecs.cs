using ProjectionTools.Specifications;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.Warehouses.Specs;

public static class WarehouseSpecs
{
    public static Specification<Warehouse> GetByProductionSite(ProductionSite item) =>
        new (x => x.ProductionSite == item);
}