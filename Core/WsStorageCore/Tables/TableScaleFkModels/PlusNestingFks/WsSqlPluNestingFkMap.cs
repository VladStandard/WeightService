// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleFkModels.PlusNestingFks;

public sealed class WsSqlPluNestingFkMap : ClassMap<WsSqlPluNestingFkModel>
{
    public WsSqlPluNestingFkMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PlusNestingFks);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.IsDefault).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_DEFAULT").Not.Nullable().Default("0");
        Map(item => item.BundleCount).CustomSqlType(WsSqlFieldTypeUtils.SmallInt).Column("BUNDLE_COUNT").Not.Nullable().Default("0").Unique();
        Map(item => item.WeightMax).CustomSqlType(WsSqlFieldTypeUtils.Decimal103).Column("WEIGHT_MAX").Not.Nullable().Unique();
        Map(item => item.WeightMin).CustomSqlType(WsSqlFieldTypeUtils.Decimal103).Column("WEIGHT_MIN").Not.Nullable().Unique();
        Map(item => item.WeightNom).CustomSqlType(WsSqlFieldTypeUtils.Decimal103).Column("WEIGHT_NOM").Not.Nullable().Unique();
        References(item => item.Box).Column("BOX_UID").Unique().Not.Nullable();
        //References(item => item.Plu).Column("PLU_BUNDLE_FK.PLU_UID").Unique().Not.Nullable();
        References(item => item.PluBundle).Column("PLU_BUNDLE_FK").Unique().Not.Nullable();
    }
}