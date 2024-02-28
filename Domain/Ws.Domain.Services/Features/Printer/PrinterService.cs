using Ws.Database.Core.Entities.Ref.Printers;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Printer.Validators;

namespace Ws.Domain.Services.Features.Printer;

internal class PrinterService(SqlPrinterRepository printerRepo) : IPrinterService
{
    [Transactional]
    public PrinterEntity GetItemByUid(Guid uid) => printerRepo.GetByUid(uid);
    
    [Transactional]
    public IEnumerable<PrinterEntity> GetAll() => printerRepo.GetAll();
    
    [Transactional, Validate<PrinterNewValidator>]
    public PrinterEntity Create(PrinterEntity item) => printerRepo.Save(item);
    
    [Transactional, Validate<PrinterUpdateValidator>]
    public PrinterEntity Update(PrinterEntity item) => printerRepo.Update(item);
}