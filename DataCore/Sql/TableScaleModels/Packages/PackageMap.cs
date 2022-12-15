// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels.Packages;

/// <summary>
/// Table map "PACKAGES".
/// </summary>
public class PackageMap : ClassMap<PackageModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PackageMap()
    {
        Schema("db_scales");
        Table("PACKAGES");
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.Name).CustomSqlType("NVARCHAR").Length(256).Column("NAME").Not.Nullable().Default("");
        Map(x => x.Weight).CustomSqlType("DECIMAL(10,3)").Column("WEIGHT").Not.Nullable().Default("0");
    }
}
