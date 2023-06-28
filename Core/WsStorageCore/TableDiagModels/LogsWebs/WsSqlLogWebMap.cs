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
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.StampDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("STAMP_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Version).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("VERSION").Length(12).Not.Nullable();
        Map(item => item.Direction).CustomSqlType(WsSqlFieldTypeUtils.TinyInt).Column("DIRECTION").Not.Nullable();
        Map(item => item.Url).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("URL").Length(512).Not.Nullable();
        Map(item => item.Params).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("PARAMS").Length(1024).Not.Nullable();
        Map(item => item.Headers).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("HEADERS").Length(1024).Not.Nullable();
        Map(item => item.DataType).CustomSqlType(WsSqlFieldTypeUtils.TinyInt).Column("DATA_TYPE").Not.Nullable();
        Map(item => item.DataString).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("DATA_STRING").Not.Nullable();
        Map(item => item.CountAll).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("COUNT_ALL").Not.Nullable();
        Map(item => item.CountSuccess).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("COUNT_SUCCESS").Not.Nullable();
        Map(item => item.CountErrors).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("COUNT_ERROR").Not.Nullable();
    }
}