using Ws.Database.Core.Entities.Ref.Printers;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Services.Features.Printer;

internal class PrinterService : IPrinterService
{
    public PrinterEntity GetByUid(Guid uid) => new SqlPrinterRepository().GetByUid(uid);
    public IEnumerable<PrinterEntity> GetAll() => new SqlPrinterRepository().GetEnumerable();
}