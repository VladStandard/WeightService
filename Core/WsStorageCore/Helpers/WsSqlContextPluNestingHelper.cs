// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Helpers;

/// <summary>
/// SQL-помощник табличных записей таблицы PLUS_NESTING_FK.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlContextPluNestingHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlContextPluNestingHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlContextPluNestingHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private static WsSqlAccessManagerHelper AccessManager => WsSqlAccessManagerHelper.Instance;
    private static WsSqlContextListHelper ContextList => WsSqlContextListHelper.Instance;
    
    #endregion

    #region Public and private methods

    /// <summary>
    /// Force update list PluStorageMethodFks.
    /// </summary>
    /// <param name="pluNestingFks"></param>
    /// <param name="sqlCrudConfig"></param>
    public List<PluNestingFkModel> UpdatePluNestingFks(SqlCrudConfigModel sqlCrudConfig, out List<PluNestingFkModel> pluNestingFks) =>
        pluNestingFks = ContextList.GetListNotNullablePlusNestingFks(sqlCrudConfig);

    /// <summary>
    /// Get item PluStorageMethod by Plu.
    /// Use UpdatePluStorageMethodFks for force update.
    /// </summary>
    /// <param name="pluNestingFks"></param>
    /// <param name="plu"></param>
    /// <param name="bundle"></param>
    /// <param name="box"></param>
    /// <returns></returns>
    public PluNestingFkModel GetPluNestingFk(List<PluNestingFkModel> pluNestingFks, PluModel plu, BundleModel bundle, BoxModel box)
    {
        PluNestingFkModel pluNestingFk = pluNestingFks.Find(item => Equals(item.PluBundle.Plu, plu) &&
                                                                    Equals(item.PluBundle.Bundle, bundle) && Equals(item.Box, box));
        return pluNestingFk.IsExists ? pluNestingFk : new();
    }

    /// <summary>
    /// Get item PluStorageMethod by Plu.
    /// Use UpdatePluStorageMethodFks for force update.
    /// </summary>
    /// <param name="pluNestingFks"></param>
    /// <param name="plu"></param>
    /// <param name="bundle"></param>
    /// <param name="box"></param>
    /// <returns></returns>
    public short GetPluNestingFkBundleCount(List<PluNestingFkModel> pluNestingFks, PluModel plu, BundleModel bundle, BoxModel box) =>
        GetPluNestingFk(pluNestingFks, plu, bundle, box).BundleCount;

    /// <summary>
    /// Get item PluStorageMethod by Plu.
    /// Use UpdatePluStorageMethodFks for force update.
    /// </summary>
    /// <param name="pluNestingFk"></param>
    /// <returns></returns>
    public short GetPluNestingFkBundleCount(PluNestingFkModel pluNestingFk) => pluNestingFk.BundleCount;

    #endregion
}