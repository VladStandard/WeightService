// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Devices;

/// <summary>
/// Table map "DEVICES".
/// </summary>
public sealed class WsSqlDeviceMap : ClassMap<WsSqlDeviceModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlDeviceMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Devices);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(item => item.LoginDt).CustomSqlType("DATETIME").Column("LOGIN_DT").Not.Nullable();
        Map(item => item.LogoutDt).CustomSqlType("DATETIME").Column("LOGOUT_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Name).CustomSqlType("NVARCHAR").Column("NAME").Length(128).Not.Nullable();
        Map(item => item.PrettyName).CustomSqlType("NVARCHAR").Column("PRETTY_NAME").Length(128).Not.Nullable();
        Map(item => item.Description).CustomSqlType("NVARCHAR").Column("DESCRIPTION").Length(1024).Not.Nullable();
        Map(item => item.Ipv4).CustomSqlType("VARCHAR").Column("IP_V4").Length(15).Nullable();
        Map(item => item.MacAddressValue).CustomSqlType("VARCHAR").Column("MAC").Length(12).Nullable();
    }
}
