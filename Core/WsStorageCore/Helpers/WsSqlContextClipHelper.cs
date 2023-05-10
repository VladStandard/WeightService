// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Helpers;

/// <summary>
/// SQL-помощник табличных записей таблицы CLIPS.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlContextClipHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlContextClipHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlContextClipHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private WsSqlAccessItemHelper AccessItem => WsSqlAccessItemHelper.Instance;
    private WsSqlContextItemHelper ContextItem => WsSqlContextItemHelper.Instance;
    private WsSqlContextListHelper ContextList => WsSqlContextListHelper.Instance;

    #endregion

    #region Public and private methods

    public WsSqlClipModel GetNewItem() => AccessItem.GetItemNewEmpty<WsSqlClipModel>();

    public WsSqlClipModel GetItem(WsSqlPluModel plu) => ContextItem.GetItemPluClipFkNotNullable(plu).Clip;
    
    public List<WsSqlClipModel> GetList() => ContextList.GetListNotNullableClips(new());

    #endregion
}