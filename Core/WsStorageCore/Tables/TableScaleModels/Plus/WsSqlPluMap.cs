namespace WsStorageCore.Tables.TableScaleModels.Plus;

public sealed class WsSqlPluMap : ClassMap<WsSqlPluModel>
{
    public WsSqlPluMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Plus);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.IsGroup).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_GROUP").Not.Nullable().Default("0");
        Map(item => item.Number).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("NUMBER").Not.Nullable();
        Map(item => item.Name).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("NAME").Length(150).Not.Nullable();
        Map(item => item.FullName).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("FULL_NAME").Not.Nullable();
        Map(item => item.Description).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("DESCRIPTION").Not.Nullable();
        Map(item => item.ShelfLifeDays).CustomSqlType(WsSqlFieldTypeUtils.TinyInt).Column("SHELF_LIFE_DAYS").Not.Nullable();
        Map(item => item.Gtin).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("GTIN").Length(14).Not.Nullable().Default(string.Empty);
        Map(item => item.Ean13).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("EAN13").Length(13).Not.Nullable().Default(string.Empty);
        Map(item => item.Itf14).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("ITF14").Length(14).Not.Nullable().Default(string.Empty);
        Map(item => item.IsCheckWeight).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_CHECK_WEIGHT").Not.Nullable().Default("1");
        Map(item => item.Code).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("CODE").Length(11).Not.Nullable();
        Map(item => item.Uid1C).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID_1C").Not.Nullable().Default(Guid.Empty.ToString());
        References(item => item.Bundle).Column("BUNDLE_UID").Not.Nullable();
    }
}