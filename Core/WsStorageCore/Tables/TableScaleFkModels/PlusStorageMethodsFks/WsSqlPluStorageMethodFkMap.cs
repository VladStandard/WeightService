using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableScaleFkModels.PlusStorageMethodsFks;

public sealed class WsSqlPluStorageMethodFkMap : ClassMap<WsSqlPluStorageMethodFkModel>
{
    public WsSqlPluStorageMethodFkMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PlusStorageMethodsFks);
        Not.LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        References(item => item.Plu).Column("PLU_UID").Not.Nullable();
        References(item => item.Method).Column("METHOD_UID").Not.Nullable();
        References(item => item.Resource).Column("RESOURCE_UID").Not.Nullable();
    }
}