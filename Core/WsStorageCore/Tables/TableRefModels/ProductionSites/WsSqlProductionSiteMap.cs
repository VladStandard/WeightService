namespace WsStorageCore.Tables.TableRefModels.ProductionSites;

/// <summary>
/// Table map "Production_sites".
/// </summary>
public sealed class WsSqlProductionSiteMap : ClassMap<WsSqlProductionSiteModel>
{
    public WsSqlProductionSiteMap()
    {
        Schema(WsSqlSchemasUtils.Ref);
        Table(WsSqlTablesUtils.ProductionSites);
        Not.LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Name).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("NAME").Length(150).Nullable();
        Map(item => item.Address).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("ADDRESS").Length(512).Nullable();
    }
}
