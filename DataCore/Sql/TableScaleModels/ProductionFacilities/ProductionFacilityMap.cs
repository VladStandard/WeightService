// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels.ProductionFacilities;

/// <summary>
/// Table map "ProductionFacility".
/// </summary>
public class ProductionFacilityMap : ClassMap<ProductionFacilityModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public ProductionFacilityMap()
    {
        Schema("db_scales");
        Table("ProductionFacility");
        LazyLoad();
        Id(x => x.IdentityValueId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CreateDate").Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("ModifiedDate").Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("Marked").Not.Nullable().Default("0");
        Map(x => x.Name).CustomSqlType("NVARCHAR").Column("Name").Length(150).Nullable();
        Map(x => x.Address).CustomSqlType("NVARCHAR").Column("ADDRESS").Length(1024).Nullable();
    }
}
