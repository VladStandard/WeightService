using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Printers;

public class SqlPrinterRepository : IGetItemByUid<PrinterEntity>, IGetAll<PrinterEntity>
{
    public PrinterEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<PrinterEntity>(uid);
    
    public IEnumerable<PrinterEntity> GetAll()
    {
        DetachedCriteria criteria = DetachedCriteria.For<PrinterEntity>()
            .AddOrder(SqlOrder.NameAsc()).AddOrder(Order.Asc(nameof(PrinterEntity.Type)));
        return SqlCoreHelper.Instance.GetEnumerable<PrinterEntity>(criteria);
    }
}