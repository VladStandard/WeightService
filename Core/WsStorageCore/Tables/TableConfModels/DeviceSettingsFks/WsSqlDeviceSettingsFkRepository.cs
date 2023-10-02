using WsStorageCore.OrmUtils;

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
        return GetEnumerableByDevice(line.Device);
    }

    public IEnumerable<WsSqlDeviceSettingsFkModel> GetEnumerableByDevice(WsSqlDeviceModel device)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(WsSqlDeviceSettingsFkModel.Device), device));
        
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