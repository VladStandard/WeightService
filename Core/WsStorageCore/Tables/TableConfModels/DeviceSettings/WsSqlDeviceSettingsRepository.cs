// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableConfModels.DeviceSettings;

/// <summary>
/// Репозиторий таблицы "DEVICES_SETTINGS".
/// </summary>
public sealed class WsSqlDeviceSettingsRepository : WsSqlTableRepositoryBase<WsSqlDeviceSettingsModel>
{
    #region Public and private methods

    public WsSqlDeviceSettingsModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlDeviceSettingsModel>();

    public WsSqlDeviceSettingsModel GetItem(Guid? uid) => SqlCore.GetItemNotNullableByUid<WsSqlDeviceSettingsModel>(uid);

    public List<WsSqlDeviceSettingsModel> GetList() => ContextList.GetListNotNullableDeviceSettings(SqlCrudConfig);
    
    public List<WsSqlDeviceSettingsModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => ContextList.GetListNotNullableDeviceSettings(sqlCrudConfig);
    
    #endregion
}