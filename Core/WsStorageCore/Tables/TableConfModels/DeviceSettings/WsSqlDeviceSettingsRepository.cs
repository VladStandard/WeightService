namespace WsStorageCore.Tables.TableConfModels.DeviceSettings;

/// <summary>
/// Репозиторий таблицы "DEVICES_SETTINGS".
/// </summary>
public sealed class WsSqlDeviceSettingsRepository : WsSqlTableRepositoryBase<WsSqlDeviceSettingsModel>
{
    #region Public and private methods

    public WsSqlDeviceSettingsModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlDeviceSettingsModel>();

    public IEnumerable<WsSqlDeviceSettingsModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(nameof(WsSqlTableBase.Name));
        return SqlCore.GetEnumerableNotNullable<WsSqlDeviceSettingsModel>(sqlCrudConfig);
    }

    #endregion
}