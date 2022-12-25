// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleFkModels.BundlesFks;

/// <summary>
/// Table map "BUNDLES_FK".
/// </summary>
public class BundleFkMap : ClassMap<BundleFkModel>
{   
    /// <summary>
    /// Constructor.
    /// </summary>
    public BundleFkMap()
    {
        Schema("db_scales");
        Table("BUNDLES_FK");
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.Name).CustomSqlType("NVARCHAR").Column("NAME").Length(128).Not.Nullable().Default("");
        Map(x => x.BundleCount).CustomSqlType("SMALLINT").Column("BUNDLE_COUNT").Not.Nullable().Default("0");
        References(x => x.Box).Column("BOX_UID").Unique().Not.Nullable();
        References(x => x.Bundle).Column("BUNDLE_UID").Unique().Not.Nullable();
  
    }
}
