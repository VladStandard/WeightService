namespace WsStorageCore.Tables.TableDiagModels.LogsTypes;

public sealed class WsSqlLogTypeMap : ClassMap<WsSqlLogTypeModel>
{
    public WsSqlLogTypeMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.LogsTypes);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Number).CustomSqlType(WsSqlFieldTypeUtils.TinyInt).Column("NUMBER").Not.Nullable();
        Map(item => item.Icon).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("ICON").Length(32).Not.Nullable();
    }
}
