// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableDiagModels.LogsMemories;

/// <summary>
/// Table map "diag.LOGS_MEMORIES".
/// </summary>
public sealed class LogMemoryMap : ClassMap<LogMemoryModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public LogMemoryMap()
    {
        Schema(WsSqlSchemasUtils.Diag);
        Table(WsSqlTablesUtils.LogsMemories);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.SizeAppMb).CustomSqlType("SMALLINT").Column("SIZE_APP_MB").Not.Nullable().Default("0");
        Map(x => x.SizeFreeMb).CustomSqlType("SMALLINT").Column("SIZE_FREE_MB").Not.Nullable().Default("0");
        References(x => x.App).Column("APP_UID").Not.Nullable();
        References(x => x.Device).Column("DEVICE_UID").Not.Nullable();
    }
}