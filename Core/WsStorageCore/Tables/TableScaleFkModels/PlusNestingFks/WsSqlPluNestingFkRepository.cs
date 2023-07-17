// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Views.ViewRefModels.PluNestings;

namespace WsStorageCore.Tables.TableScaleFkModels.PlusNestingFks;

/// <summary>
/// Контроллер таблицы PLUS_NESTING_FK.
/// </summary>
public sealed class WsSqlPluNestingFkRepository : WsSqlTableRepositoryBase<WsSqlPluNestingFkModel>
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlPluNestingFkRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlPluNestingFkRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private WsSqlBoxRepository ContextBox => WsSqlBoxRepository.Instance;
    private WsSqlPluRepository ContextPlu => WsSqlPluRepository.Instance;
    private WsSqlPluBundleFkRepository ContextPluBundle => WsSqlPluBundleFkRepository.Instance;

    #endregion

    #region Public and private methods

    public WsSqlPluNestingFkModel GetNewItem()
    {
        WsSqlPluNestingFkModel item = SqlCore.GetItemNewEmpty<WsSqlPluNestingFkModel>();
        item.Box = ContextBox.GetNewItem();
        item.PluBundle = ContextPluBundle.GetNewItem();
        return item;
    }

    public WsSqlViewPluNestingModel GetNewView() => new();

    /// <summary>
    /// Force update list.
    /// </summary>
    /// <param name="pluNestingFks"></param>
    /// <param name="sqlCrudConfig"></param>
    public List<WsSqlPluNestingFkModel> UpdatePluNestingFks(WsSqlCrudConfigModel sqlCrudConfig, 
        out List<WsSqlPluNestingFkModel> pluNestingFks) =>
        pluNestingFks = ContextList.GetListNotNullablePlusNestingFks(sqlCrudConfig);

    /// <summary>
    /// Get item by Plu.
    /// Use UpdatePluStorageMethodFks for force update.
    /// </summary>
    /// <param name="pluNestingFks"></param>
    /// <param name="plu"></param>
    /// <param name="bundle"></param>
    /// <param name="box"></param>
    /// <returns></returns>
    public WsSqlPluNestingFkModel GetPluNestingFk(List<WsSqlPluNestingFkModel> pluNestingFks, WsSqlPluModel plu, WsSqlBundleModel bundle, WsSqlBoxModel box)
    {
        WsSqlPluNestingFkModel pluNestingFk = pluNestingFks.Find(item => Equals(item.PluBundle.Plu, plu) &&
                                                                    Equals(item.PluBundle.Bundle, bundle) && Equals(item.Box, box));
        return pluNestingFk.IsExists ? pluNestingFk : new();
    }

    /// <summary>
    /// Get item by Plu.
    /// Use UpdatePluStorageMethodFks for force update.
    /// </summary>
    /// <param name="pluNestingFks"></param>
    /// <param name="plu"></param>
    /// <param name="bundle"></param>
    /// <param name="box"></param>
    /// <returns></returns>
    public short GetPluNestingFkBundleCount(List<WsSqlPluNestingFkModel> pluNestingFks, WsSqlPluModel plu, WsSqlBundleModel bundle, WsSqlBoxModel box) =>
        GetPluNestingFk(pluNestingFks, plu, bundle, box).BundleCount;

    /// <summary>
    /// Get item PluStorageMethod by Plu.
    /// Use UpdatePluStorageMethodFks for force update.
    /// </summary>
    /// <param name="pluNestingFk"></param>
    /// <returns></returns>
    public short GetPluNestingFkBundleCount(WsSqlPluNestingFkModel pluNestingFk) => pluNestingFk.BundleCount;

    public List<WsSqlPluNestingFkModel> GetList() => ContextList.GetListNotNullablePlusNestingFks(SqlCrudConfig);

    public List<WsSqlPluNestingFkModel> GetListByUid(Guid? uid)
    {
        uid ??= Guid.Empty;
        WsSqlCrudConfigModel sqlCrudConfig = new()
        {
            NativeParameters = new() { new("P_UID", uid) },
            NativeQuery = PluNestingFks.GetList(true)
        };
        List<WsSqlPluNestingFkModel> result = ContextList.GetListNotNullablePlusNestingFks(sqlCrudConfig);
        return result;
    }

    public List<WsSqlPluNestingFkModel> GetListByNumber(short number)
    {
        WsSqlPluModel plu = ContextPlu.GetItemByNumber(number);
        return GetListByUid(plu.IdentityValueUid);
    }

    #endregion
}