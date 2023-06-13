// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Scales;

/// <summary>
/// SQL-помощник табличных записей таблицы SCALES.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlLineController
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlLineController _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlLineController Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private WsSqlAccessItemHelper AccessItem => WsSqlAccessItemHelper.Instance;
    private WsSqlContextListHelper ContextList => WsSqlContextListHelper.Instance;

    #endregion

    #region Public and private methods

    public WsSqlScaleModel GetNewItem() => AccessItem.GetItemNewEmpty<WsSqlScaleModel>();

    public WsSqlScaleModel GetItem(Guid uid) => AccessItem.GetItemNotNullable<WsSqlScaleModel>(uid);

    public List<WsSqlScaleModel> GetList() => ContextList.GetListNotNullableLines(new());

    public List<WsSqlScaleModel> GetList(WsSqlIsMarked isMarked) =>
        ContextList.GetListNotNullableLines(new() { IsMarked = isMarked, IsResultOrder = true });

    #endregion
}