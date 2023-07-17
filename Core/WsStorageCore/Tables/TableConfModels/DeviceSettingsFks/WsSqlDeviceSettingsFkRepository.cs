// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableConfModels.DeviceSettingsFks;

/// <summary>
/// Репозиторий таблицы "DEVICES_SETTINGS_FK".
/// </summary>
public sealed class WsSqlDeviceSettingsFkRepository : WsSqlTableRepositoryBase<WsSqlDeviceSettingsFkModel>
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlDeviceSettingsFkRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlDeviceSettingsFkRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private methods

    public WsSqlDeviceSettingsFkModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlDeviceSettingsFkModel>();

    public WsSqlDeviceSettingsFkModel GetItem(Guid? uid) => SqlCore.GetItemNotNullableByUid<WsSqlDeviceSettingsFkModel>(uid);

    public List<WsSqlDeviceSettingsFkModel> GetList() => ContextList.GetListNotNullableDeviceSettingsFks(SqlCrudConfig);

    public void SaveItem(WsSqlDeviceSettingsFkModel item) => SqlCore.Save(item);

    public void SaveItemAsync(WsSqlDeviceSettingsFkModel item) => SqlCore.Save(item, WsSqlEnumSessionType.IsolatedAsync);

    #endregion
}