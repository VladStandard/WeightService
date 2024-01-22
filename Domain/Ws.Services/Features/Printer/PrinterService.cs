using Ws.Database.Core.Entities.Ref.Printers;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Services.Features.Printer;

internal class PrinterService : IPrinterService
{
    public IEnumerable<PrinterEntity> GetAll() => new SqlPrinterRepository().GetEnumerable();

    public PrinterEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<PrinterEntity>(uid);
}