// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableConfModels.DeviceSettings;

/// <summary>
/// Репозиторий таблицы "DEVICES_SETTINGS".
/// </summary>
public sealed class WsSqlDeviceSettingsRepository : WsSqlTableRepositoryBase<WsSqlDeviceSettingsModel>
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlDeviceSettingsRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlDeviceSettingsRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private methods

    public WsSqlDeviceSettingsModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlDeviceSettingsModel>();

    public WsSqlDeviceSettingsModel GetItem(Guid? uid) => SqlCore.GetItemNotNullableByUid<WsSqlDeviceSettingsModel>(uid);

    public List<WsSqlDeviceSettingsModel> GetList() => ContextList.GetListNotNullableDeviceSettings(SqlCrudConfig);
    
    public List<WsSqlDeviceSettingsModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => ContextList.GetListNotNullableDeviceSettings(sqlCrudConfig);
    
    #endregion
}