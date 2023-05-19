// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Access;

/// <summary>
/// SQL-помощник табличных записей таблицы ACCESS.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlAccessController
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlAccessController _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlAccessController Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private WsSqlAccessItemHelper AccessItem => WsSqlAccessItemHelper.Instance;
    private WsSqlContextItemHelper ContextItem => WsSqlContextItemHelper.Instance;
    private WsSqlContextListHelper ContextList => WsSqlContextListHelper.Instance;

    #endregion

    #region Public and private methods

    public WsSqlAccessModel GetNewItem() => AccessItem.GetItemNewEmpty<WsSqlAccessModel>();

    public WsSqlAccessModel GetItem(Guid uid) => AccessItem.GetItemNotNullable<WsSqlAccessModel>(uid);

    public List<WsSqlAccessModel> GetList() => ContextList.GetListNotNullableAccesses(new());

    public List<WsSqlAccessModel> GetList(WsSqlIsMarked isMarked) =>
        ContextList.GetListNotNullableAccesses(new() { IsMarked = isMarked, IsResultOrder = true });

    #endregion
}