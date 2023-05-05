// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusNestingFks;

/// <summary>
/// Маппинг полей таблицы PLUS_NESTING_FK.
/// </summary>
public sealed class WsSqlPluNestingFkMap : ClassMap<WsSqlPluNestingFkModel>
{   
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPluNestingFkMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PlusNestingFks);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.IsDefault).CustomSqlType("BIT").Column("IS_DEFAULT").Not.Nullable().Default("0");
        Map(x => x.BundleCount).CustomSqlType("SMALLINT").Column("BUNDLE_COUNT").Not.Nullable().Default("0").Unique();
        Map(x => x.WeightMax).CustomSqlType("DECIMAL(10,3)").Column("WEIGHT_MAX").Not.Nullable().Unique();
        Map(x => x.WeightMin).CustomSqlType("DECIMAL(10,3)").Column("WEIGHT_MIN").Not.Nullable().Unique();
        Map(x => x.WeightNom).CustomSqlType("DECIMAL(10,3)").Column("WEIGHT_NOM").Not.Nullable().Unique();
        References(x => x.Box).Column("BOX_UID").Unique().Not.Nullable();
        //References(x => x.Plu).Column("PLU_BUNDLE_FK.PLU_UID").Unique().Not.Nullable();
        References(x => x.PluBundle).Column("PLU_BUNDLE_FK").Unique().Not.Nullable();
    }
}