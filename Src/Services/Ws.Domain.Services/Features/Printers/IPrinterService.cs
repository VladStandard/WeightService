using Ws.Domain.Models.Entities.Devices;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.Printers;

public interface IPrinterService : IGetItemByUid<Printer>, ICreate<Printer>,
    IUpdate<Printer>, IDelete<Guid>
{
    IList<Printer> GetAllByProductionSite(ProductionSite site);
}