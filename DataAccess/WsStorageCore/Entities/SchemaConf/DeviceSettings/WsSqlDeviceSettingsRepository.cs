using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaConf.DeviceSettings;

/// <summary>
/// Репозиторий таблицы "DEVICES_SETTINGS".
/// </summary>
public sealed class WsSqlDeviceSettingsRepository : WsSqlTableRepositoryBase<WsSqlDeviceSettingsEntity>
{
    #region Public and private methods

    public WsSqlDeviceSettingsEntity GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlDeviceSettingsEntity>();

    public IEnumerable<WsSqlDeviceSettingsEntity> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<WsSqlDeviceSettingsEntity>(sqlCrudConfig);
    }

    #endregion
}