using Ws.StorageCore.Common;
using Ws.StorageCore.Models;
using Ws.StorageCore.OrmUtils;
namespace Ws.StorageCore.Entities.SchemaRef.Printers;

public class SqlPrinterRepository : SqlTableRepositoryBase<SqlPrinterEntity>
{
    public IEnumerable<SqlPrinterEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlPrinterEntity>(sqlCrudConfig);
    }

}