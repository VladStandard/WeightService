using ProjectionTools.Specifications;
using Ws.Domain.Models.Entities.Devices;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.Printers.Specs;

internal static class PrinterSpecs
{
    public static Specification<Printer> GetByProductionSite(ProductionSite item) =>
        new (x => x.ProductionSite == item);
}