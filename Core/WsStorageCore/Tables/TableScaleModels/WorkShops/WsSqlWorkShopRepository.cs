namespace WsStorageCore.Tables.TableScaleModels.WorkShops;

public sealed class WsSqlWorkShopRepository : WsSqlTableRepositoryBase<WsSqlWorkShopModel>
{
    #region Item
    
    public WsSqlWorkShopModel GetItemByUid(Guid uid) => SqlCore.GetItemNotNullable<WsSqlWorkShopModel>(uid);
    
    public WsSqlWorkShopModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlWorkShopModel>();
    
    #endregion

    #region List
    
    public List<WsSqlWorkShopModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => ContextList.GetListNotNullableWorkShops(sqlCrudConfig);
    
    #endregion

}