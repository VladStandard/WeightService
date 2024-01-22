using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Printers;

public class SqlPrinterRepository : SqlTableRepositoryBase<PrinterEntity>
{
    public IEnumerable<PrinterEntity> GetEnumerable()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.NameAsc());
        IEnumerable<PrinterEntity> items = SqlCore.GetEnumerable<PrinterEntity>(crud);
        return items.OrderBy(item => item.Type);
    }
}