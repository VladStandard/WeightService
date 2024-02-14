using Ws.Database.Core.Entities.Ref.Printers;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.Printer;

internal class PrinterService(SqlPrinterRepository printerRepo) : IPrinterService
{
    public PrinterEntity GetItemByUid(Guid uid) => printerRepo.GetByUid(uid);
    public IEnumerable<PrinterEntity> GetAll() => printerRepo.GetAll();
}