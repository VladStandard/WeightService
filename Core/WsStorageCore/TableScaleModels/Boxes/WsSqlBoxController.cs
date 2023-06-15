// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Boxes;

/// <summary>
/// SQL-контроллер таблицы BOXES.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlBoxController : WsSqlTableControllerBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlBoxController _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlBoxController Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private WsSqlAccessItemHelper AccessItem => WsSqlAccessItemHelper.Instance;
    private WsSqlContextItemHelper ContextItem => WsSqlContextItemHelper.Instance;
    private WsSqlContextListHelper ContextList => WsSqlContextListHelper.Instance;

    #endregion

    #region Public and private methods

    public WsSqlBoxModel GetNewItem() => AccessItem.GetItemNewEmpty<WsSqlBoxModel>();

    //public WsSqlBoxModel GetItem(WsSqlPluModel plu) => ContextItem.GetItemPluBundleFkNotNullable(plu);

    public List<WsSqlBoxModel> GetList() => ContextList.GetListNotNullableBoxes(SqlCrudConfig);

    #endregion
}