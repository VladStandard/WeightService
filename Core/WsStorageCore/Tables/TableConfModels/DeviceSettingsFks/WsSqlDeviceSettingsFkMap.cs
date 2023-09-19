namespace WsStorageCore.Tables.TableConfModels.DeviceSettingsFks;

/// <summary>
/// Маппинг таблицы "DEVICES_SETTINGS_FK".
/// </summary>
public sealed class WsSqlDeviceSettingsFkMap : ClassMap<WsSqlDeviceSettingsFkModel>
{
    public WsSqlDeviceSettingsFkMap()
    {
        Schema(WsSqlSchemasUtils.Conf);
        Table(WsSqlTablesUtils.DeviceSettingsFks);
        Not.LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.IsEnabled).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_ENABLED").Not.Nullable().Default("0");
        References(item => item.Device).Column("DEVICE_UID").Not.Nullable();
        References(item => item.Setting).Column("SETTING_UID").Not.Nullable();
    }
}