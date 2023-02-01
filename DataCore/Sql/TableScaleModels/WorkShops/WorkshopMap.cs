// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Utils;

namespace DataCore.Sql.TableScaleModels.WorkShops;

/// <summary>
/// Table map "WorkShop".
/// </summary>
public class WorkShopMap : ClassMap<WorkShopModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WorkShopMap()
    {
        Schema(SqlSchemaNamesUtils.DbScales);
        Table(SqlTableNamesUtils.WorkShops);
        LazyLoad();
        Id(x => x.IdentityValueId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CreateDate").Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("ModifiedDate").Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("Marked").Not.Nullable().Default("0");
        Map(x => x.Name).CustomSqlType("NVARCHAR").Column("Name").Not.Nullable().Length(150);
        References(x => x.ProductionFacility).Column("ProductionFacilityID").Not.Nullable();
    }
}
