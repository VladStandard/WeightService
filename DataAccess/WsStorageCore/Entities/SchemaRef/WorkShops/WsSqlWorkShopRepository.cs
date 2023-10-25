using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaRef.WorkShops;

public sealed class WsSqlWorkShopRepository : WsSqlTableRepositoryBase<WsSqlWorkShopEntity>
{
    #region Public and private methods

    public WsSqlWorkShopEntity GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlWorkShopEntity>();

    public IEnumerable<WsSqlWorkShopEntity> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<WsSqlWorkShopEntity>(sqlCrudConfig);
    }

    #endregion
}