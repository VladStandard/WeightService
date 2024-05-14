using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Printer;

public interface IPrinterService : IGetItemByUid<Models.Entities.Devices.Printer>, ICreate<Models.Entities.Devices.Printer>,
    IUpdate<Models.Entities.Devices.Printer>, IDelete<Models.Entities.Devices.Printer>
{
    IEnumerable<Models.Entities.Devices.Printer> GetAllByProductionSite(Models.Entities.Ref.ProductionSite site);
}