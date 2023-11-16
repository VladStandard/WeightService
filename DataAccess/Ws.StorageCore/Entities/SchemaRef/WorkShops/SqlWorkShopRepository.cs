using Ws.StorageCore.Common;
using Ws.StorageCore.Models;
using Ws.StorageCore.OrmUtils;
namespace Ws.StorageCore.Entities.SchemaRef.WorkShops;

public sealed class SqlWorkShopRepository : SqlTableRepositoryBase<SqlWorkShopEntity>
{
    #region Public and private methods

    public SqlWorkShopEntity GetNewItem() => SqlCore.GetItemNewEmpty<SqlWorkShopEntity>();

    public IEnumerable<SqlWorkShopEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlWorkShopEntity>(sqlCrudConfig);
    }

    #endregion
}