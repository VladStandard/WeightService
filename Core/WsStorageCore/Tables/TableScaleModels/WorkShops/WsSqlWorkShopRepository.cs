namespace WsStorageCore.Tables.TableScaleModels.WorkShops;

public sealed class WsSqlWorkShopRepository : WsSqlTableRepositoryBase<WsSqlWorkShopModel>
{
    #region Item
    
    public WsSqlWorkShopModel GetItemById(long id) => SqlCore.GetItemNotNullable<WsSqlWorkShopModel>(id);
    
    public WsSqlWorkShopModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlWorkShopModel>();
    
    #endregion

    #region List

    public List<WsSqlWorkShopModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlWorkShopModel> list = SqlCore.GetListNotNullable<WsSqlWorkShopModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }
    
    #endregion
}