using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Printers;

public class SqlPrinterRepository : IGetItemByUid<PrinterEntity>, IGetAll<PrinterEntity>
{
    public PrinterEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<PrinterEntity>(uid);
    
    public IEnumerable<PrinterEntity> GetAll()
    {
        return SqlCoreHelper.Instance.GetEnumerable<PrinterEntity>(
            DetachedCriteria.For<PrinterEntity>()
                .AddOrder(SqlOrder.NameAsc()).AddOrder(Order.Asc(nameof(PrinterEntity.Type)))
        );
    }
}