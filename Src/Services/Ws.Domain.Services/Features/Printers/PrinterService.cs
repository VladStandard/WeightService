using Ws.Database.Nhibernate.Entities.Ref.Printers;
using Ws.Domain.Models.Entities.Devices;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Printers.Specs;
using Ws.Domain.Services.Features.Printers.Validators;

namespace Ws.Domain.Services.Features.Printers;

internal class PrinterService(SqlPrinterRepository printerRepo) : IPrinterService
{
    #region Items

    [Transactional]
    public Printer GetItemByUid(Guid uid) => printerRepo.GetByUid(uid);

    #endregion

    #region List

    [Transactional]
    public IList<Printer> GetAllByProductionSite(ProductionSite site) =>
        printerRepo.GetListBySpec(PrinterSpecs.GetByProductionSite(site));

    #endregion

    #region CRUD

    [Transactional, Validate<PrinterNewValidator>]
    public Printer Create(Printer item) => printerRepo.Save(item);

    [Transactional, Validate<PrinterUpdateValidator>]
    public Printer Update(Printer item) => printerRepo.Update(item);

    [Transactional]
    public void DeleteById(Guid id) => printerRepo.Delete(new() { Uid = id });

    #endregion
}