// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableConfModels.DeviceSettingsFks;

/// <summary>
/// Маппинг таблицы "DEVICES_SETTINGS_FK".
/// </summary>
public sealed class WsSqlDeviceSettingsFkMap : ClassMap<WsSqlDeviceSettingsFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlDeviceSettingsFkMap()
    {
        Schema(WsSqlSchemasUtils.Conf);
        Table(WsSqlTablesUtils.DeviceSettingsFks);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.IsEnabled).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_ENABLED").Not.Nullable().Default("0");
        References(item => item.Device).Column("DEVICE_UID").Not.Nullable();
        References(item => item.Setting).Column("SETTING_UID").Not.Nullable();
    }
}