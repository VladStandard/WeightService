namespace WsStorageCore.Tables.TableRef1cModels.Brands;

public sealed class WsSqlBrandMap : ClassMap<WsSqlBrandModel>
{
    public WsSqlBrandMap()
    {
        Schema(WsSqlSchemasUtils.Ref1C);
        Table(WsSqlTablesUtils.Brands);
        Not.LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Name).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("NAME").Length(128).Not.Nullable();
        Map(item => item.Code).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("CODE").Length(128).Not.Nullable();
        Map(item => item.Uid1C).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID_1C").Not.Nullable().Default(Guid.Empty.ToString());
    }
}