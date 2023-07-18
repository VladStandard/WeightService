namespace WsStorageCore.Tables.TableScaleModels.WorkShops;

public sealed class WsSqlWorkShopRepository : WsSqlTableRepositoryBase<WsSqlWorkShopModel>
{
    public List<WsSqlWorkShopModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => ContextList.GetListNotNullableWorkShops(sqlCrudConfig);
}