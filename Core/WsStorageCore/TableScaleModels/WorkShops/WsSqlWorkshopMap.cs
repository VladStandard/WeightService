// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.WorkShops;

/// <summary>
/// Table map "WorkShop".
/// </summary>
public sealed class WsSqlWorkshopMap : ClassMap<WsSqlWorkShopModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlWorkshopMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.WorkShops);
        LazyLoad();
        Id(item => item.IdentityValueId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME").Column("CreateDate").Nullable();
        Map(item => item.ChangeDt).CustomSqlType("DATETIME").Column("ModifiedDate").Nullable();
        Map(item => item.IsMarked).CustomSqlType("BIT").Column("Marked").Not.Nullable().Default("0");
        Map(item => item.Name).CustomSqlType("NVARCHAR").Column("Name").Not.Nullable().Length(150);
        References(item => item.ProductionFacility).Column("ProductionFacilityID").Not.Nullable();
    }
}