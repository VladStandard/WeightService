using Ws.Domain.Models.Entities.Ref;
using Ws.StorageCore.Entities.Ref.Printers;
using Ws.StorageCore.Helpers;

namespace Ws.Services.Features.Printer;

internal class PrinterService : IPrinterService
{
    public IEnumerable<PrinterEntity> GetAll() => new SqlPrinterRepository().GetEnumerable();

    public PrinterEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<PrinterEntity>(uid);
}