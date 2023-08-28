namespace WsStorageCore.Tables.TableScaleFkModels.PlusBrandsFks;

public sealed class WsSqlPluBrandFkMap : ClassMap<WsSqlPluBrandFkModel>
{
    public WsSqlPluBrandFkMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PlusBrandsFks);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        References(item => item.Plu).Column("PLU_UID").Unique().Not.Nullable();
        References(item => item.Brand).Column("BRAND_UID").Not.Nullable();
    }
}