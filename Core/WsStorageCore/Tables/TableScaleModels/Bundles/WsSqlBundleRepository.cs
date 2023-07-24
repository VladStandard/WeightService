// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.Bundles;

/// <summary>
/// SQL-контроллер таблицы BUNDLES.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlBundleRepository : WsSqlTableRepositoryBase<WsSqlBundleModel>
{
    #region Public and private methods

    public WsSqlBundleModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlBundleModel>();

    public WsSqlBundleModel GetItemByPlu(WsSqlPluModel plu)
    {
        if (plu.IsNew)
            return new();
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
            $"{nameof(WsSqlPluBundleFkModel.Plu)}.{nameof(WsSqlTableBase.IdentityValueUid)}", plu.IdentityValueUid, WsSqlEnumIsMarked.ShowAll, false);
        return SqlCore.GetItemNotNullable<WsSqlPluBundleFkModel>(sqlCrudConfig).Bundle;
    }
    
    public WsSqlBundleModel GetItemByUid1C(Guid uid1C)
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new() { new() { Name = nameof(WsSqlTable1CBase.Uid1C), Value = uid1C } },
            WsSqlEnumIsMarked.ShowAll, false, false, false);
        return SqlCore.GetItemNotNullable<WsSqlBundleModel>(sqlCrudConfig);
    }
    
    public List<WsSqlBundleModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlBundleModel> list = SqlCore.GetListNotNullable<WsSqlBundleModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    #endregion
}