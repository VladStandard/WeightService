using ProjectionTools.Specifications;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.Arms.Specs;

internal static class ArmSpecs
{
    public static Specification<Arm> GetByProductionSite(ProductionSite item) =>
        new (x => x.Warehouse.ProductionSite == item);

    public static Specification<Arm> GetByPcName(string pcName) =>
        new (x => x.PcName == pcName);
}