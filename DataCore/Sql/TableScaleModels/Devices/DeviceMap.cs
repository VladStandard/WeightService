// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Utils;

namespace DataCore.Sql.TableScaleModels.Devices;

/// <summary>
/// Table map "DEVICES".
/// </summary>
public sealed class DeviceMap : ClassMap<DeviceModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public DeviceMap()
    {
        Schema(SqlSchemaNamesUtils.DbScales);
        Table(SqlTableNamesUtils.Devices);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.LoginDt).CustomSqlType("DATETIME").Column("LOGIN_DT").Not.Nullable();
        Map(x => x.LogoutDt).CustomSqlType("DATETIME").Column("LOGOUT_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.Name).CustomSqlType("NVARCHAR").Column("NAME").Length(128).Not.Nullable();
        Map(x => x.PrettyName).CustomSqlType("NVARCHAR").Column("PRETTY_NAME").Length(128).Not.Nullable();
        Map(x => x.Description).CustomSqlType("NVARCHAR").Column("DESCRIPTION").Length(1024).Not.Nullable();
        Map(x => x.Ipv4).CustomSqlType("VARCHAR").Column("IP_V4").Length(15).Nullable();
        Map(x => x.MacAddressValue).CustomSqlType("VARCHAR").Column("MAC").Length(12).Nullable();
    }
}
