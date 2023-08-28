namespace WsStorageCore.Tables.TableScaleModels.PlusWeighings;

public sealed class WsSqlPluWeighingMap : ClassMap<WsSqlPluWeighingModel>
{
    public WsSqlPluWeighingMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PlusWeightings);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Kneading).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("KNEADING").Not.Nullable().Default("0");
        Map(item => item.Sscc).CustomSqlType(WsSqlFieldTypeUtils.VarChar).Column("SSCC").Length(50).Not.Nullable().Default("");
        Map(item => item.NettoWeight).CustomSqlType(WsSqlFieldTypeUtils.Decimal103).Column("NETTO_WEIGHT").Not.Nullable().Default("0");
        Map(item => item.WeightTare).CustomSqlType(WsSqlFieldTypeUtils.Decimal103).Column("TARE_WEIGHT").Not.Nullable().Default("0");
        Map(item => item.RegNum).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("REG_NUM").Not.Nullable().Default("0");
        References(item => item.PluScale).Column("PLU_SCALE_UID").Not.Nullable();
        References(item => item.Series).Column("SERIES_ID").Nullable();
    }
}
