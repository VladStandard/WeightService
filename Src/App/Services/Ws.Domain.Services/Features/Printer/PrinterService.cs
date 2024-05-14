using Ws.Database.Nhibernate.Entities.Ref.Printers;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Printer.Validators;

namespace Ws.Domain.Services.Features.Printer;

internal class PrinterService(SqlPrinterRepository printerRepo) : IPrinterService
{
    [Transactional]
    public Models.Entities.Devices.Printer GetItemByUid(Guid uid) => printerRepo.GetByUid(uid);

    [Transactional, Validate<PrinterNewValidator>]
    public Models.Entities.Devices.Printer Create(Models.Entities.Devices.Printer item) => printerRepo.Save(item);

    [Transactional, Validate<PrinterUpdateValidator>]
    public Models.Entities.Devices.Printer Update(Models.Entities.Devices.Printer item) => printerRepo.Update(item);

    [Transactional]
    public void Delete(Models.Entities.Devices.Printer item) => printerRepo.Delete(item);

    [Transactional]
    public IEnumerable<Models.Entities.Devices.Printer> GetAllByProductionSite(Models.Entities.Ref.ProductionSite site) =>
        printerRepo.GetAllByProductionSite(site);
}