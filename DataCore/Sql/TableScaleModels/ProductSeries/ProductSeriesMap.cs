// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Utils;

namespace DataCore.Sql.TableScaleModels.ProductSeries;

/// <summary>
/// Table map "ProductSeries".
/// </summary>
public sealed class ProductSeriesMap : ClassMap<ProductSeriesModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public ProductSeriesMap()
    {
        Schema(WsSqlSchemaNamesUtils.DbScales);
        Table(WsSqlTableNamesUtils.ProductSeries);
        LazyLoad();
        Id(x => x.IdentityValueId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME(2,7)").Column("CreateDate").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.Uid).CustomSqlType("UNIQUEIDENTIFIER").Column("UUID").Nullable();
        Map(x => x.IsClose).CustomSqlType("BIT").Column("IsClose").Not.Nullable().Default("0");
        Map(x => x.Sscc).CustomSqlType("VARCHAR").Column("SSCC").Length(50).Nullable();
        References(x => x.Scale).Column("ScaleID").Not.Nullable();
    }
}
