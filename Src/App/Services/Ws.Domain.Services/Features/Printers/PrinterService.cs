using Ws.Database.Nhibernate.Entities.Ref.Printers;
using Ws.Domain.Models.Entities.Devices;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Printers.Validators;

namespace Ws.Domain.Services.Features.Printers;

internal class PrinterService(SqlPrinterRepository printerRepo) : IPrinterService
{
    [Transactional]
    public Printer GetItemByUid(Guid uid) => printerRepo.GetByUid(uid);

    [Transactional, Validate<PrinterNewValidator>]
    public Printer Create(Printer item) => printerRepo.Save(item);

    [Transactional, Validate<PrinterUpdateValidator>]
    public Printer Update(Printer item) => printerRepo.Update(item);

    [Transactional]
    public void Delete(Printer item) => printerRepo.Delete(item);

    [Transactional]
    public IEnumerable<Printer> GetAllByProductionSite(ProductionSite site) =>
        printerRepo.GetAllByProductionSite(site);
}