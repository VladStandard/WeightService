using WsStorageCore.Entities.SchemaRef.Hosts;
using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaConf.DeviceSettingsFks;

/// <summary>
/// Репозиторий таблицы "DEVICES_SETTINGS_FK".
/// </summary>
public sealed class WsSqlDeviceSettingsFkRepository : WsSqlTableRepositoryBase<WsSqlDeviceSettingsFkEntity>
{
    #region Public and private methods

    public IEnumerable<WsSqlDeviceSettingsFkEntity> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<WsSqlDeviceSettingsFkEntity> items =
            SqlCore.GetEnumerable<WsSqlDeviceSettingsFkEntity>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items
                .OrderBy(item => item.Host.Name)
                .ThenBy(item => item.Setting.Name);
        return items;
    }

    public IEnumerable<WsSqlDeviceSettingsFkEntity> GetEnumerableByLine(WsSqlScaleEntity line)
    {
        return GetEnumerableByDevice(line.Host);
    }

    public IEnumerable<WsSqlDeviceSettingsFkEntity> GetEnumerableByDevice(WsSqlHostEntity host)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(WsSqlDeviceSettingsFkEntity.Host), host));

        IEnumerable<WsSqlDeviceSettingsFkEntity> items =
            SqlCore.GetEnumerable<WsSqlDeviceSettingsFkEntity>(sqlCrudConfig);
        items = items
            .OrderBy(item => item.Host.Name)
            .ThenBy(item => item.Setting.Name);
        return items;
    }

    public WsSqlDeviceSettingsFkEntity GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlDeviceSettingsFkEntity>();

    public void SaveItem(WsSqlDeviceSettingsFkEntity item) => SqlCore.Save(item);

    public void SaveItemAsync(WsSqlDeviceSettingsFkEntity item) =>
        SqlCore.Save(item, WsSqlEnumSessionType.IsolatedAsync);

    public void UpdateItem(WsSqlDeviceSettingsFkEntity item) => SqlCore.Update(item);

    public void UpdateItemAsync(WsSqlDeviceSettingsFkEntity item) =>
        SqlCore.Update(item, WsSqlEnumSessionType.IsolatedAsync);

    #endregion
}