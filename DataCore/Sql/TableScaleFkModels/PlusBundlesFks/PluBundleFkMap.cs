// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleFkModels.PlusBundlesFks;

/// <summary>
/// Table map "PLUS_BUNDLES_FK".
/// </summary>
public class PluBundleFkMap : ClassMap<PluBundleFkModel>
{   
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluBundleFkMap()
    {
        Schema("db_scales");
        Table("PLUS_BUNDLES_FK");
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        References(x => x.BundleFk).Column("BUNDLE_FK_UID").Unique().Not.Nullable();
        References(x => x.Plu).Column("PLU_UID").Unique().Not.Nullable();
    }
}