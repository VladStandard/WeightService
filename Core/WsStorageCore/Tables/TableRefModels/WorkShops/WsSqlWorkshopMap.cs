using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableRefModels.WorkShops;

/// <summary>
/// Table map "WorkShop".
/// </summary>
public sealed class WsSqlWorkshopMap : ClassMap<WsSqlWorkShopModel>
{
    public WsSqlWorkshopMap()
    {
        Schema(WsSqlSchemasUtils.Ref);
        Table(WsSqlTablesUtils.WorkShops);
        Not.LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Name).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("NAME").Not.Nullable().Length(128);
        References(item => item.ProductionSite).Column("PRODUCTION_SITES_UID").Not.Nullable();
    }
}