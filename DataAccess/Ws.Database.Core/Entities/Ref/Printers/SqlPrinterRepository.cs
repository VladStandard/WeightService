using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Printers;

public class SqlPrinterRepository : IUidRepo<PrinterEntity>
{
    public PrinterEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<PrinterEntity>(uid);
    
    public IEnumerable<PrinterEntity> GetEnumerable()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.NameAsc());
        IEnumerable<PrinterEntity> items = SqlCoreHelper.Instance.GetEnumerable<PrinterEntity>(crud);
        return items.OrderBy(item => item.Type);
    }
}