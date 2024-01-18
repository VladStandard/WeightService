namespace Ws.StorageCore.Entities.SchemaRef.Printers;

public class SqlPrinterRepository : SqlTableRepositoryBase<SqlPrinterEntity>
{
    public IEnumerable<SqlPrinterEntity> GetEnumerable()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.NameAsc());
        IEnumerable<SqlPrinterEntity> items = SqlCore.GetEnumerable<SqlPrinterEntity>(crud);
        return items.OrderBy(item => item.Type);
    }
}