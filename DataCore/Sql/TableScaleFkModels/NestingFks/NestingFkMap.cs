// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleFkModels.NestingFks;

/// <summary>
/// Table map "BUNDLES_FK".
/// </summary>
public class NestingFkMap : ClassMap<NestingFkModel>
{   
    /// <summary>
    /// Constructor.
    /// </summary>
    public NestingFkMap()
    {
        Schema("db_scales");
        Table("NESTING_FK");
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.Name).CustomSqlType("NVARCHAR").Column("NAME").Length(128).Not.Nullable().Default("");
        Map(x => x.BundleCount).CustomSqlType("SMALLINT").Column("BUNDLE_COUNT").Not.Nullable().Default("0");
        Map(x => x.WeightMax).CustomSqlType("DECIMAL(10,3)").Column("WEIGHT_MAX").Not.Nullable();
        Map(x => x.WeightMin).CustomSqlType("DECIMAL(10,3)").Column("WEIGHT_MIN").Not.Nullable();
        Map(x => x.WeightNom).CustomSqlType("DECIMAL(10,3)").Column("WEIGHT_NOM").Not.Nullable();
        References(x => x.Box).Column("BOX_UID").Unique().Not.Nullable();
    }
}