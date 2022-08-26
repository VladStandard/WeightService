// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table map "WorkShop".
/// </summary>
public class WorkShopMap : ClassMap<WorkShopEntity>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WorkShopMap()
    {
        Schema("db_scales");
        Table("WorkShop");
        LazyLoad();
        Id(x => x.IdentityId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CreateDate").Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("ModifiedDate").Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("Marked").Not.Nullable().Default("0");
        Map(x => x.Name).CustomSqlType("NVARCHAR").Column("Name").Not.Nullable().Length(150);
        Map(x => x.IdRRef).CustomSqlType("UNIQUEIDENTIFIER").Column("IdRRef").Nullable();
        References(x => x.ProductionFacility).Column("ProductionFacilityID").Not.Nullable();
    }
}
