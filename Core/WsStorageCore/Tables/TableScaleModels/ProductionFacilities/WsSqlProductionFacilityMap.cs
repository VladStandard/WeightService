namespace WsStorageCore.Tables.TableScaleModels.ProductionFacilities;

/// <summary>
/// Table map "ProductionFacility".
/// </summary>
public sealed class WsSqlProductionFacilityMap : ClassMap<WsSqlProductionFacilityModel>
{
    public WsSqlProductionFacilityMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.ProductionFacilities);
        LazyLoad();
        Id(item => item.IdentityValueId).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CreateDate").Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("ModifiedDate").Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("Marked").Not.Nullable().Default("0");
        Map(item => item.Name).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("Name").Length(150).Nullable();
        Map(item => item.Address).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("ADDRESS").Length(1024).Nullable();
    }
}
