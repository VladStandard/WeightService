// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Helpers;

/// <summary>
/// SQL-помощник табличных записей таблиц PLUS_STORAGE_METHODS, PLUS_STORAGE_METHODS_FK.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlContextPluStorageHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlContextPluStorageHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlContextPluStorageHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private static WsSqlAccessManagerHelper AccessManager => WsSqlAccessManagerHelper.Instance;
    private static WsSqlContextListHelper ContextList => WsSqlContextListHelper.Instance;

    #endregion

    #region Public and private methods

    /// <summary>
    /// Force update list PluStorageMethodFks.
    /// </summary>
    /// <param name="sqlCrudConfig"></param>
    /// <param name="pluStorageMethodsFks"></param>
    public List<PluStorageMethodFkModel> UpdatePluStorageMethodFks(SqlCrudConfigModel sqlCrudConfig) =>
        ContextList.GetListNotNullablePlusStoragesMethodsFks(sqlCrudConfig);

    /// <summary>
    /// Get item PluStorageMethod by Plu.
    /// Use UpdatePluStorageMethodFks for force update.
    /// </summary>
    /// <param name="plu"></param>
    /// <param name="pluStorageMethodsFks"></param>
    /// <returns></returns>
    public PluStorageMethodModel GetPluStorageMethod(WsSqlPluModel plu, List<PluStorageMethodFkModel> pluStorageMethodsFks)
    {
        PluStorageMethodFkModel pluStorageMethodFk = new();
        if (pluStorageMethodsFks.Exists(item => Equals(item.Plu, plu)))
            pluStorageMethodFk = pluStorageMethodsFks.Find(item => Equals(item.Plu, plu));
        return pluStorageMethodFk.Method;
    }

    /// <summary>
    /// Get item PluStorageMethod by Plu.
    /// Use UpdatePluStorageMethodFks for force update.
    /// </summary>
    /// <param name="plu"></param>
    /// <param name="pluStorageMethodsFks"></param>
    /// <returns></returns>
    public TemplateResourceModel GetPluStorageResource(WsSqlPluModel plu, List<PluStorageMethodFkModel> pluStorageMethodsFks)
    {
        PluStorageMethodFkModel pluStorageMethodFk = new();
        if (pluStorageMethodsFks.Exists(item => Equals(item.Plu, plu)))
            pluStorageMethodFk = pluStorageMethodsFks.Find(item => Equals(item.Plu, plu));
        return pluStorageMethodFk.Resource;
    }

    /// <summary>
    /// Get item PluStorageMethodFk by Plu.
    /// Use UpdatePluStorageMethodFks for force update.
    /// </summary>
    /// <param name="plu"></param>
    /// <param name="pluStorageMethodsFks"></param>
    /// <returns></returns>
    public PluStorageMethodFkModel GetPluStorageMethodFk(WsSqlPluModel plu, List<PluStorageMethodFkModel> pluStorageMethodsFks)
    {
        PluStorageMethodFkModel pluStorageMethodFk = new();
        if (pluStorageMethodsFks.Exists(item => Equals(item.Plu, plu)))
            pluStorageMethodFk = pluStorageMethodsFks.Find(item => Equals(item.Plu, plu));
        return pluStorageMethodFk;
    }

    #endregion
}