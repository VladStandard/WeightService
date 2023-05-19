// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.ProductionFacilities;

/// <summary>
/// Table map "ProductionFacility".
/// </summary>
public sealed class WsSqlProductionFacilityMap : ClassMap<WsSqlProductionFacilityModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlProductionFacilityMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.ProductionFacilities);
        LazyLoad();
        Id(item => item.IdentityValueId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME").Column("CreateDate").Nullable();
        Map(item => item.ChangeDt).CustomSqlType("DATETIME").Column("ModifiedDate").Nullable();
        Map(item => item.IsMarked).CustomSqlType("BIT").Column("Marked").Not.Nullable().Default("0");
        Map(item => item.Name).CustomSqlType("NVARCHAR").Column("Name").Length(150).Nullable();
        Map(item => item.Address).CustomSqlType("NVARCHAR").Column("ADDRESS").Length(1024).Nullable();
    }
}
