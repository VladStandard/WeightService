using Ws.Database.Core.Entities.Ref.Printers;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Printer;

public class PrinterService(SqlPrinterRepository printerRepo) : IPrinterService
{
    [Transactional] public PrinterEntity GetItemByUid(Guid uid) => printerRepo.GetByUid(uid);
    [Transactional] public IEnumerable<PrinterEntity> GetAll() => printerRepo.GetAll();
    [Transactional] public PrinterEntity Create(PrinterEntity item) => printerRepo.Save(item);
    [Transactional] public PrinterEntity Update(PrinterEntity item) => printerRepo.Update(item);
}