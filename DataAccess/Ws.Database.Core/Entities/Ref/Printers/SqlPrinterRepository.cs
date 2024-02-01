using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Printers;

public class SqlPrinterRepository : IGetItemByUid<PrinterEntity>, IGetAll<PrinterEntity>
{
    public PrinterEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<PrinterEntity>(uid);
    
    public IEnumerable<PrinterEntity> GetAll()
    {
        return SqlCoreHelper.Instance.GetEnumerable(
            QueryOver.Of<PrinterEntity>().OrderBy(i => i.Type).Asc
        );
    }
}