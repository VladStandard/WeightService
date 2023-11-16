using Ws.StorageCore.Common;
using Ws.StorageCore.Models;
using Ws.StorageCore.OrmUtils;
namespace Ws.StorageCore.Entities.SchemaScale.Versions;

public class SqlVersionRepository : SqlTableRepositoryBase<SqlVersionEntity>
{
    public List<SqlVersionEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.Desc(nameof(SqlVersionEntity.Version)));
        return SqlCore.GetEnumerable<SqlVersionEntity>(sqlCrudConfig).ToList();
    }
}