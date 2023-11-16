using Ws.StorageCore.Common;
using Ws.StorageCore.Models;
using Ws.StorageCore.OrmUtils;
namespace Ws.StorageCore.Entities.SchemaScale.PlusStorageMethods;

public class SqlPluStorageMethodRepository : SqlTableRepositoryBase<SqlPluStorageMethodEntity>
{
    public List<SqlPluStorageMethodEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlPluStorageMethodEntity>(sqlCrudConfig).ToList();
    }
}