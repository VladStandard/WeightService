// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table map "ProductSeries".
/// </summary>
public class ProductSeriesMap : ClassMap<ProductSeriesEntity>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public ProductSeriesMap()
    {
        Schema("db_scales");
        Table("ProductSeries");
        LazyLoad();
        Id(x => x.IdentityId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        References(x => x.Scale).Column("ScaleID").Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME(2,7)").Column("CreateDate").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.IdentityUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UUID").Nullable();
        Map(x => x.IsClose).CustomSqlType("BIT").Column("IsClose").Not.Nullable().Default("0");
        Map(x => x.Sscc).CustomSqlType("VARCHAR").Column("SSCC").Length(50).Nullable();
    }
}
