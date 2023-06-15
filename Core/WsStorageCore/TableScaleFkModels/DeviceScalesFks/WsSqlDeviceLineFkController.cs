// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.DeviceScalesFks;

/// <summary>
/// SQL-контроллер таблицы DEVICES_SCALES_FK.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlDeviceLineFkController : WsSqlTableControllerBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlDeviceLineFkController _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlDeviceLineFkController Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private WsSqlAccessItemHelper AccessItem => WsSqlAccessItemHelper.Instance;
    private WsSqlContextItemHelper ContextItem => WsSqlContextItemHelper.Instance;
    private WsSqlContextListHelper ContextList => WsSqlContextListHelper.Instance;
    private WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;

    #endregion

    #region Public and private methods

    public WsSqlDeviceScaleFkModel GetNewItem() => AccessItem.GetItemNewEmpty<WsSqlDeviceScaleFkModel>();

    public WsSqlDeviceScaleFkModel GetItem(Guid? uid) => AccessItem.GetItemNotNullableByUid<WsSqlDeviceScaleFkModel>(uid);

    public List<WsSqlDeviceScaleFkModel> GetList() => ContextList.GetListNotNullableDeviceScalesFks(SqlCrudConfig);

    #endregion
}