// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableDiagModels.Logs;

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
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Version).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("VERSION").Length(12).Nullable();
        Map(item => item.File).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("FILE").Length(40).Not.Nullable();
        Map(item => item.Line).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("LINE").Not.Nullable();
        Map(item => item.Member).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("MEMBER").Length(40).Not.Nullable();
        Map(item => item.Message).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("MESSAGE").Length(1024).Not.Nullable();
        References(item => item.Device).Column("DEVICE_UID").Nullable();
        References(item => item.App).Column("APP_UID").Nullable();
        References(item => item.LogType).Column("LOG_TYPE_UID").Nullable();
    }
}