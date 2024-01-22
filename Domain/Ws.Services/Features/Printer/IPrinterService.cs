using Ws.Domain.Models.Entities.Ref;
using Ws.Services.Common;

namespace Ws.Services.Features.Printer;

public interface IPrinterService : IAll<PrinterEntity>, IUid<PrinterEntity>;