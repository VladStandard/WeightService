// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableDiagModels.LogsWebs;

/// <summary>
/// Table map "LOGS_WEBS".
/// </summary>
public sealed class WsSqlLogWebMap : ClassMap<WsSqlLogWebModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlLogWebMap()
    {
        Schema(WsSqlSchemasUtils.Diag);
        Table(WsSqlTablesUtils.LogsWebs);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(item => item.StampDt).CustomSqlType("DATETIME").Column("STAMP_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Version).CustomSqlType("NVARCHAR").Column("VERSION").Length(12).Not.Nullable();
        Map(item => item.Direction).CustomSqlType("TINYINT").Column("DIRECTION").Not.Nullable();
        Map(item => item.Url).CustomSqlType("NVARCHAR").Column("URL").Length(512).Not.Nullable();
        Map(item => item.Params).CustomSqlType("NVARCHAR").Column("PARAMS").Length(1024).Not.Nullable();
        Map(item => item.Headers).CustomSqlType("NVARCHAR").Column("HEADERS").Length(1024).Not.Nullable();
        Map(item => item.DataType).CustomSqlType("TINYINT").Column("DATA_TYPE").Not.Nullable();
        Map(item => item.DataString).CustomSqlType("NVARCHAR").Column("DATA_STRING").Not.Nullable();
        Map(item => item.CountAll).CustomSqlType("INT").Column("COUNT_ALL").Not.Nullable();
        Map(item => item.CountSuccess).CustomSqlType("INT").Column("COUNT_SUCCESS").Not.Nullable();
        Map(item => item.CountErrors).CustomSqlType("INT").Column("COUNT_ERROR").Not.Nullable();
    }
}