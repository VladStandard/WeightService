// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableConfModels.DeviceSettingsFks;

/// <summary>
/// Репозиторий таблицы "DEVICES_SETTINGS_FK".
/// </summary>
public sealed class WsSqlDeviceSettingsFkRepository : WsSqlTableRepositoryBase<WsSqlDeviceSettingsFkModel>
{
    #region Public and private methods

    public List<WsSqlDeviceSettingsFkModel> GetList() => GetList(SqlCrudConfig);

    public List<WsSqlDeviceSettingsFkModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlDeviceSettingsFkModel> list = SqlCore.GetListNotNullable<WsSqlDeviceSettingsFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            list = list
                .OrderBy(item => item.Device.Name)
                .ThenBy(item => item.Setting.Name).ToList();
        return list;
    }
    
    public WsSqlDeviceSettingsFkModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlDeviceSettingsFkModel>();
    
    public void SaveItem(WsSqlDeviceSettingsFkModel item) => SqlCore.Save(item);
   
    public void SaveItemAsync(WsSqlDeviceSettingsFkModel item) => SqlCore.Save(item, WsSqlEnumSessionType.IsolatedAsync);

    #endregion
}