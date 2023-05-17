// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.DeviceTypesFks;

/// <summary>
/// Table map "DEVICES_TYPES_FK".
/// </summary>
public sealed class WsSqlDeviceTypeFkMap : ClassMap<WsSqlDeviceTypeFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlDeviceTypeFkMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.DevicesTypesFks);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        References(item => item.Device).Column("DEVICE_UID").Not.Nullable();
        References(item => item.Type).Column("TYPE_UID").Not.Nullable();
    }
}