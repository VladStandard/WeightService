namespace WsStorageCore.Tables.TableScaleModels.PlusLabels;

public sealed class WsSqlPluLabelMap : ClassMap<WsSqlPluLabelModel>
{
    public WsSqlPluLabelMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PlusLabels);
        Not.LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Zpl).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("ZPL").Not.Nullable().Default("");
        Map(item => item.Xml).CustomSqlType(WsSqlFieldTypeUtils.Xml).Column("XML").Nullable();
        Map(item => item.ProductDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("PROD_DT").Not.Nullable();
        Map(item => item.ExpirationDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("EXPIRATION_DT").Not.Nullable();
        References(item => item.PluWeighing).Column("PLU_WEIGHING_UID").Nullable();
        References(item => item.PluScale).Column("PLU_SCALE_UID").Nullable();
    }
}