// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleFkModels.DeviceScalesFks;

/// <summary>
/// SQL-контроллер таблицы DEVICES_SCALES_FK.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlDeviceLineFkRepository : WsSqlTableRepositoryBase<WsSqlDeviceScaleFkModel>
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlDeviceLineFkRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlDeviceLineFkRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private methods

    public WsSqlDeviceScaleFkModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlDeviceScaleFkModel>();

    public WsSqlDeviceScaleFkModel GetItem(Guid? uid) => SqlCore.GetItemNotNullableByUid<WsSqlDeviceScaleFkModel>(uid);

    public List<WsSqlDeviceScaleFkModel> GetList() => ContextList.GetListNotNullableDeviceScalesFks(SqlCrudConfig);
    
    public List<WsSqlDeviceScaleFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => ContextList.GetListNotNullableDeviceScalesFks(sqlCrudConfig);
    
    #endregion
}