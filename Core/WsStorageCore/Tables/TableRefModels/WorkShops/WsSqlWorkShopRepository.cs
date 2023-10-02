using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableRefModels.WorkShops;

public sealed class WsSqlWorkShopRepository : WsSqlTableRepositoryBase<WsSqlWorkShopModel>
{
    #region Public and private methods

    public WsSqlWorkShopModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlWorkShopModel>();

    public IEnumerable<WsSqlWorkShopModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerableNotNullable<WsSqlWorkShopModel>(sqlCrudConfig);
    }

    public async Task<IEnumerable<WsSqlWorkShopModel>> GetEnumerableAsync(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return await SqlCore.GetEnumerableNotNullableAsync<WsSqlWorkShopModel>(sqlCrudConfig);
    }

    #endregion
}