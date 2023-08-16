// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableConfModels.DeviceSettingsFks;

/// <summary>
/// Репозиторий таблицы "DEVICES_SETTINGS_FK".
/// </summary>
public sealed class WsSqlDeviceSettingsFkRepository : WsSqlTableRepositoryBase<WsSqlDeviceSettingsFkModel>
{
    #region Public and private methods
    
    public IEnumerable<WsSqlDeviceSettingsFkModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<WsSqlDeviceSettingsFkModel> items = SqlCore.GetEnumerableNotNullable<WsSqlDeviceSettingsFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items
                .OrderBy(item => item.Device.Name)
                .ThenBy(item => item.Setting.Name);
        return items;
    }

    public IEnumerable<WsSqlDeviceSettingsFkModel> GetEnumerableByLine(WsSqlScaleModel line)
    {
        WsSqlDeviceModel device = WsSqlContextManagerHelper.Instance.DeviceRepository.GetItemByLine(line);
        return GetEnumerableByDevice(device);
    }

    public IEnumerable<WsSqlDeviceSettingsFkModel> GetEnumerableByDevice(WsSqlDeviceModel device)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFkIdentityFilter(nameof(WsSqlDeviceSettingsFkModel.Device), device);
        IEnumerable<WsSqlDeviceSettingsFkModel> items = SqlCore.GetEnumerableNotNullable<WsSqlDeviceSettingsFkModel>(sqlCrudConfig);
        items = items
            .OrderBy(item => item.Device.Name)
            .ThenBy(item => item.Setting.Name);
        return items;
    }

    public WsSqlDeviceSettingsFkModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlDeviceSettingsFkModel>();
    
    public void SaveItem(WsSqlDeviceSettingsFkModel item) => SqlCore.Save(item);
   
    public void SaveItemAsync(WsSqlDeviceSettingsFkModel item) => SqlCore.Save(item, WsSqlEnumSessionType.IsolatedAsync);

    public void UpdateItem(WsSqlDeviceSettingsFkModel item) => SqlCore.Update(item);
   
    public void UpdateItemAsync(WsSqlDeviceSettingsFkModel item) => SqlCore.Update(item, WsSqlEnumSessionType.IsolatedAsync);

    #endregion
}