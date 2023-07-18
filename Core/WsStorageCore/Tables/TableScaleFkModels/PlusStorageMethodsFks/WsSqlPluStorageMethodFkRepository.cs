// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleFkModels.PlusStorageMethodsFks;

/// <summary>
/// SQL-контроллер таблицы записей таблиц PLUS_STORAGE_METHODS, PLUS_STORAGE_METHODS_FK.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluStorageMethodFkRepository : WsSqlTableRepositoryBase<WsSqlPluStorageMethodFkModel>
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlPluStorageMethodFkRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlPluStorageMethodFkRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private methods

    /// <summary>
    /// Force update list PluStorageMethodFks.
    /// </summary>
    /// <param name="sqlCrudConfig"></param>
    public List<WsSqlPluStorageMethodFkModel> GetListFks(WsSqlCrudConfigModel sqlCrudConfig) =>
        ContextList.GetListNotNullablePlusStoragesMethodsFks(sqlCrudConfig);

    /// <summary>
    /// Get item PluStorageMethod by Plu.
    /// Use UpdatePluStorageMethodFks for force update.
    /// </summary>
    /// <param name="plu"></param>
    /// <param name="pluStorageMethodsFks"></param>
    /// <returns></returns>
    public WsSqlPluStorageMethodModel GetItem(WsSqlPluModel plu, List<WsSqlPluStorageMethodFkModel> pluStorageMethodsFks)
    {
        WsSqlPluStorageMethodFkModel pluStorageMethodFk = new();
        if (pluStorageMethodsFks.Exists(item => Equals(item.Plu, plu)))
            pluStorageMethodFk = pluStorageMethodsFks.Find(item => Equals(item.Plu, plu));
        return pluStorageMethodFk.Method;
    }

    /// <summary>
    /// Get item PluStorageMethod by Plu.
    /// Use UpdatePluStorageMethodFks for force update.
    /// </summary>
    /// <param name="plu"></param>
    /// <returns></returns>
    public WsSqlTemplateResourceModel GetItemResource(WsSqlPluModel plu)
    {
        WsSqlPluStorageMethodFkModel pluStorageMethodFk = new();
        if (ContextCache.ViewPlusStorageMethods.Exists(item => Equals(item.PluNumber, (ushort)plu.Number)))
        {
            pluStorageMethodFk = ContextItem.GetItemPluStorageMethodFkNotNullable(plu.IdentityValueUid);
        }
        return pluStorageMethodFk.Resource;
    }

    /// <summary>
    /// Get item PluStorageMethodFk by Plu.
    /// Use UpdatePluStorageMethodFks for force update.
    /// </summary>
    /// <param name="plu"></param>
    /// <param name="pluStorageMethodsFks"></param>
    /// <returns></returns>
    public WsSqlPluStorageMethodFkModel GetItemFk(WsSqlPluModel plu, List<WsSqlPluStorageMethodFkModel> pluStorageMethodsFks)
    {
        WsSqlPluStorageMethodFkModel pluStorageMethodFk = new();
        if (pluStorageMethodsFks.Exists(item => Equals(item.Plu.Number, plu.Number)))
            pluStorageMethodFk = pluStorageMethodsFks.Find(item => Equals(item.Plu.Number, plu.Number));
        return pluStorageMethodFk;
    }
    
    public List<WsSqlPluStorageMethodFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => ContextList.GetListNotNullablePlusStoragesMethodsFks(sqlCrudConfig);

    #endregion
}