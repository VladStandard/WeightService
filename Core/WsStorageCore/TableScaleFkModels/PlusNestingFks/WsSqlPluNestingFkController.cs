// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusNestingFks;

/// <summary>
/// Контроллер таблицы PLUS_NESTING_FK.
/// </summary>
public sealed class WsSqlPluNestingFkController
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlPluNestingFkController _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlPluNestingFkController Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private WsSqlAccessItemHelper AccessItem => WsSqlAccessItemHelper.Instance;
    private WsSqlAccessListHelper AccessList => WsSqlAccessListHelper.Instance;
    private WsSqlContextItemHelper ContextItem => WsSqlContextItemHelper.Instance;
    private WsSqlContextListHelper ContextList => WsSqlContextListHelper.Instance;
    private WsSqlContextBoxHelper ContextBox => WsSqlContextBoxHelper.Instance;
    private WsSqlPluController ContextPlu => WsSqlPluController.Instance;
    private WsSqlContextPluBundleHelper ContextPluBundle => WsSqlContextPluBundleHelper.Instance;

    #endregion

    #region Public and private methods

    public PluNestingFkModel GetNewItem()
    {
        PluNestingFkModel item = AccessItem.GetItemNewEmpty<PluNestingFkModel>();
        item.Box = ContextBox.GetNewItem();
        item.PluBundle = ContextPluBundle.GetNewItem();
        return item;
    }

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
    public PluNestingFkModel GetPluNestingFk(List<PluNestingFkModel> pluNestingFks, WsSqlPluModel plu, BundleModel bundle, BoxModel box)
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
    public short GetPluNestingFkBundleCount(List<PluNestingFkModel> pluNestingFks, WsSqlPluModel plu, BundleModel bundle, BoxModel box) =>
        GetPluNestingFk(pluNestingFks, plu, bundle, box).BundleCount;

    /// <summary>
    /// Get item PluStorageMethod by Plu.
    /// Use UpdatePluStorageMethodFks for force update.
    /// </summary>
    /// <param name="pluNestingFk"></param>
    /// <returns></returns>
    public short GetPluNestingFkBundleCount(PluNestingFkModel pluNestingFk) => pluNestingFk.BundleCount;

    public List<PluNestingFkModel> GetList() => ContextList.GetListNotNullablePlusNestingFks(new());

    public List<PluNestingFkModel> GetListByUid(Guid? uid)
    {
        uid ??= Guid.Empty;
        SqlCrudConfigModel sqlCrudConfig = new()
        {
            NativeParameters = new() { new("P_UID", uid) },
            NativeQuery = PluNestingFks.GetList(true)
        };
        List<PluNestingFkModel> result = ContextList.GetListNotNullablePlusNestingFks(sqlCrudConfig);
        return result;
    }

    public List<PluNestingFkModel> GetListByNumber(short number)
    {
        WsSqlPluModel plu = ContextPlu.GetItemByNumber(number);
        return GetListByUid(plu.IdentityValueUid);
    }

    #endregion
}