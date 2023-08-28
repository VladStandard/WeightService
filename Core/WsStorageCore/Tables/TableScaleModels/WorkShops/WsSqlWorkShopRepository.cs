namespace WsStorageCore.Tables.TableScaleModels.WorkShops;

public sealed class WsSqlWorkShopRepository : WsSqlTableRepositoryBase<WsSqlWorkShopModel>
{
    #region Public and private methods

    public WsSqlWorkShopModel GetItemById(long id) => SqlCore.GetItemById<WsSqlWorkShopModel>(id);

    public WsSqlWorkShopModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlWorkShopModel>();

    public IEnumerable<WsSqlWorkShopModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(nameof(WsSqlTableBase.Name));
        return SqlCore.GetEnumerableNotNullable<WsSqlWorkShopModel>(sqlCrudConfig);
    }

    public async Task<IEnumerable<WsSqlWorkShopModel>> GetEnumerableAsync(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(nameof(WsSqlTableBase.Name));
        return await SqlCore.GetEnumerableNotNullableAsync<WsSqlWorkShopModel>(sqlCrudConfig);
    }

    #endregion
}