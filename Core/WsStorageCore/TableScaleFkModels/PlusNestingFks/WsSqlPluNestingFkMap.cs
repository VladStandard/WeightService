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
        Id(item => item.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.IsDefault).CustomSqlType("BIT").Column("IS_DEFAULT").Not.Nullable().Default("0");
        Map(item => item.BundleCount).CustomSqlType("SMALLINT").Column("BUNDLE_COUNT").Not.Nullable().Default("0").Unique();
        Map(item => item.WeightMax).CustomSqlType("DECIMAL(10,3)").Column("WEIGHT_MAX").Not.Nullable().Unique();
        Map(item => item.WeightMin).CustomSqlType("DECIMAL(10,3)").Column("WEIGHT_MIN").Not.Nullable().Unique();
        Map(item => item.WeightNom).CustomSqlType("DECIMAL(10,3)").Column("WEIGHT_NOM").Not.Nullable().Unique();
        References(item => item.Box).Column("BOX_UID").Unique().Not.Nullable();
        //References(item => item.Plu).Column("PLU_BUNDLE_FK.PLU_UID").Unique().Not.Nullable();
        References(item => item.PluBundle).Column("PLU_BUNDLE_FK").Unique().Not.Nullable();
    }
}