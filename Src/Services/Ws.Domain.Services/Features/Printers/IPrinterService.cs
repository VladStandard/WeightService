using Ws.Domain.Models.Entities.Devices;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Printers;

public interface IPrinterService : IGetItemByUid<Printer>, ICreate<Printer>,
    IUpdate<Printer>, IDelete<Printer>
{
    IList<Printer> GetAllByProductionSite(ProductionSite site);
}