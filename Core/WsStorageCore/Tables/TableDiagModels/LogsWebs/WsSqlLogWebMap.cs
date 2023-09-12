namespace WsStorageCore.Tables.TableDiagModels.LogsWebs;

public sealed class WsSqlLogWebMap : ClassMap<WsSqlLogWebModel>
{
    public WsSqlLogWebMap()
    {
        Schema(WsSqlSchemasUtils.Diag);
        Table(WsSqlTablesUtils.LogsWebs);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.StampDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("STAMP_DT").Not.Nullable();
        Map(item => item.Version).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("VERSION").Length(12).Not.Nullable();
        Map(item => item.Url).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("URL").Length(512).Not.Nullable();
        Map(item => item.DataRequest).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("DATA_REQUEST").Not.Nullable();
        Map(item => item.DataResponse).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("DATA_RESPONSE").Not.Nullable();
        Map(item => item.CountAll).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("COUNT_ALL").Not.Nullable();
        Map(item => item.CountSuccess).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("COUNT_SUCCESS").Not.Nullable();
        Map(item => item.CountErrors).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("COUNT_ERROR").Not.Nullable();
    }
}