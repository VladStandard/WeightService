using Ws.Database.Core.Entities.Ref.Printers;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Printer;

internal class PrinterService(SqlPrinterRepository printerRepo) : IPrinterService
{
    [Session] public PrinterEntity GetItemByUid(Guid uid) => printerRepo.GetByUid(uid);
    [Session] public IEnumerable<PrinterEntity> GetAll() => printerRepo.GetAll();
}