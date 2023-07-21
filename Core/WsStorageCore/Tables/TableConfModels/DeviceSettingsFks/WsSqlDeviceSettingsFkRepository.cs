// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableConfModels.DeviceSettingsFks;

/// <summary>
/// Репозиторий таблицы "DEVICES_SETTINGS_FK".
/// </summary>
public sealed class WsSqlDeviceSettingsFkRepository : WsSqlTableRepositoryBase<WsSqlDeviceSettingsFkModel>
{
    #region Public and private methods

    public List<WsSqlDeviceSettingsFkModel> GetList() => ContextList.GetListNotNullableDeviceSettingsFks(SqlCrudConfig);
    
    public List<WsSqlDeviceSettingsFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) => ContextList.GetListNotNullableDeviceSettingsFks(sqlCrudConfig);
    
    public WsSqlDeviceSettingsFkModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlDeviceSettingsFkModel>();
    
    public WsSqlDeviceSettingsFkModel GetItem(Guid? uid) => SqlCore.GetItemNotNullableByUid<WsSqlDeviceSettingsFkModel>(uid);

    public void SaveItem(WsSqlDeviceSettingsFkModel item) => SqlCore.Save(item);
   
    public void SaveItemAsync(WsSqlDeviceSettingsFkModel item) => SqlCore.Save(item, WsSqlEnumSessionType.IsolatedAsync);

    #endregion
}