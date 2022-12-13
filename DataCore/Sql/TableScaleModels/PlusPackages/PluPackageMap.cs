// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels.PlusPackages;

/// <summary>
/// Table map "PLUS_PACKAGES".
/// </summary>
public class PluPackageMap : ClassMap<PluPackageModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluPackageMap()
    {
        Schema("db_scales");
        Table("PLUS_PACKAGES");
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.IsActive).CustomSqlType("BIT").Column("IS_ACTIVE").Not.Nullable().Default("0");
        Map(x => x.Name).CustomSqlType("NVARCHAR").Length(256).Column("NAME").Not.Nullable().Default("");
        References(x => x.Package).Column("PACKAGE_UID").Not.Nullable();
        References(x => x.Plu).Column("PLU_UID").Not.Nullable();
    }
}
