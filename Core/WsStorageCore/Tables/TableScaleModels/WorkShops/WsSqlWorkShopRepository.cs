// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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

    #endregion
}