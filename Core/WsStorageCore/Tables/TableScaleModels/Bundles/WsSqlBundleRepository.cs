// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.Bundles;

/// <summary>
/// SQL-контроллер таблицы BUNDLES.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlBundleRepository : WsSqlTableRepositoryBase<WsSqlBundleModel>
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlBundleRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlBundleRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private methods

    public WsSqlBundleModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlBundleModel>();

    public WsSqlBundleModel GetItem(WsSqlPluModel plu) => ContextItem.GetItemPluBundleFkNotNullable(plu).Bundle;

    public List<WsSqlBundleModel> GetList() => ContextList.GetListNotNullableBundles(SqlCrudConfig);

    /// <summary>
    /// Получить пакет по полю UID_1C.
    /// </summary>
    /// <param name="uid1C"></param>
    /// <returns></returns>
    public WsSqlBundleModel GetItemByUid1C(Guid uid1C)
    {
        WsSqlCrudConfigModel sqlCrudConfig = new(new() { new() { Name = nameof(WsSqlTable1CBase.Uid1C), Value = uid1C } },
            WsSqlEnumIsMarked.ShowAll, false, false, false);
        return SqlCore.GetItemNotNullable<WsSqlBundleModel>(sqlCrudConfig);
    }

    #endregion
}