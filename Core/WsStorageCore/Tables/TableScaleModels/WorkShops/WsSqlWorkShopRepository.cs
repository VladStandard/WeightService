namespace WsStorageCore.Tables.TableScaleModels.WorkShops;

public sealed class WsSqlWorkShopRepository : WsSqlTableRepositoryBase<WsSqlWorkShopModel>
{
    #region Item
    
    public WsSqlWorkShopModel GetItemById(long id) => SqlCore.GetItemById<WsSqlWorkShopModel>(id);
    
    public WsSqlWorkShopModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlWorkShopModel>();
    
    #endregion

    #region List

    public List<WsSqlWorkShopModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(new(nameof(WsSqlTableBase.Name), WsSqlEnumOrder.Asc));
        return SqlCore.GetListNotNullable<WsSqlWorkShopModel>(sqlCrudConfig);
    }
    
    #endregion
}