using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Common.Commands;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Printer;

public interface IPrinterService : IGetAll<PrinterEntity>, IGetItemByUid<PrinterEntity>, ICreate<PrinterEntity>,
    IUpdate<PrinterEntity>, IDelete<PrinterEntity>;