namespace Ws.StorageCore.Entities.SchemaRef.Printers;

public class SqlPrinterRepository : SqlTableRepositoryBase<SqlPrinterEntity>
{
    public IEnumerable<SqlPrinterEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        IEnumerable<SqlPrinterEntity> items = SqlCore.GetEnumerable<SqlPrinterEntity>(sqlCrudConfig);
        return items.OrderBy(item => item.Type);
    }
}