// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableDiagModels.Logs;

/// <summary>
/// Table map "LOGS".
/// </summary>
public sealed class WsSqlLogMap : ClassMap<WsSqlLogModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlLogMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Logs);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Version).CustomSqlType("NVARCHAR").Column("VERSION").Length(12).Nullable();
        Map(item => item.File).CustomSqlType("NVARCHAR").Column("FILE").Length(40).Not.Nullable();
        Map(item => item.Line).CustomSqlType("INT").Column("LINE").Not.Nullable();
        Map(item => item.Member).CustomSqlType("NVARCHAR").Column("MEMBER").Length(40).Not.Nullable();
        Map(item => item.Message).CustomSqlType("NVARCHAR").Column("MESSAGE").Length(1024).Not.Nullable();
        References(item => item.Device).Column("DEVICE_UID").Nullable();
        References(item => item.App).Column("APP_UID").Nullable();
        References(item => item.LogType).Column("LOG_TYPE_UID").Nullable();
    }
}