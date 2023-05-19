// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableDiagModels.LogsMemories;

/// <summary>
/// Table map "diag.LOGS_MEMORIES".
/// </summary>
public sealed class WsSqlLogMemoryMap : ClassMap<WsSqlLogMemoryModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlLogMemoryMap()
    {
        Schema(WsSqlSchemasUtils.Diag);
        Table(WsSqlTablesUtils.LogsMemories);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(item => item.SizeAppMb).CustomSqlType("SMALLINT").Column("SIZE_APP_MB").Not.Nullable().Default("0");
        Map(item => item.SizeFreeMb).CustomSqlType("SMALLINT").Column("SIZE_FREE_MB").Not.Nullable().Default("0");
        References(item => item.App).Column("APP_UID").Not.Nullable();
        References(item => item.Device).Column("DEVICE_UID").Not.Nullable();
    }
}