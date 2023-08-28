namespace WsStorageCore.Tables.TableScaleModels.WorkShops;

/// <summary>
/// Table map "WorkShop".
/// </summary>
public sealed class WsSqlWorkshopMap : ClassMap<WsSqlWorkShopModel>
{
    public WsSqlWorkshopMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.WorkShops);
        LazyLoad();
        Id(item => item.IdentityValueId).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CreateDate").Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("ModifiedDate").Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("Marked").Not.Nullable().Default("0");
        Map(item => item.Name).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("Name").Not.Nullable().Length(150);
        References(item => item.ProductionFacility).Column("ProductionFacilityID").Not.Nullable();
    }
}