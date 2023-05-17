// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.ProductSeries;

/// <summary>
/// Table map "ProductSeries".
/// </summary>
public sealed class WsSqlProductSeriesMap : ClassMap<WsSqlProductSeriesModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlProductSeriesMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.ProductSeries);
        LazyLoad();
        Id(item => item.IdentityValueId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME(2,7)").Column("CreateDate").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Uid).CustomSqlType("UNIQUEIDENTIFIER").Column("UUID").Nullable();
        Map(item => item.IsClose).CustomSqlType("BIT").Column("IsClose").Not.Nullable().Default("0");
        Map(item => item.Sscc).CustomSqlType("VARCHAR").Column("SSCC").Length(50).Nullable();
        References(item => item.Scale).Column("ScaleID").Not.Nullable();
    }
}
