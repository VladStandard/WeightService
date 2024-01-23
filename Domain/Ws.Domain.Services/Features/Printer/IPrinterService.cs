using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Common;

namespace Ws.Domain.Services.Features.Printer;

public interface IPrinterService : IAll<PrinterEntity>, IUid<PrinterEntity>;