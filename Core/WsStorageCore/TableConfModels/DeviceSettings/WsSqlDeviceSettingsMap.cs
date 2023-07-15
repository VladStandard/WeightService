// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableConfModels.DeviceSettings;

/// <summary>
/// Маппинг таблицы "DEVICES_SETTINGS".
/// </summary>
public sealed class WsSqlDeviceSettingsMap : ClassMap<WsSqlDeviceSettingsModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlDeviceSettingsMap()
    {
        Schema(WsSqlSchemasUtils.Conf);
        Table(WsSqlTablesUtils.DeviceSettings);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("DT_CREATE").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("DT_CHANGE").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Name).CustomSqlType(WsSqlFieldTypeUtils.VarChar).Length(128).Column("NAME").Not.Nullable().Default("");
    }
}