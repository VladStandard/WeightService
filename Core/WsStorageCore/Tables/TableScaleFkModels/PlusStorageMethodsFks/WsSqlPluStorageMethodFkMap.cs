// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleFkModels.PlusStorageMethodsFks;

public sealed class WsSqlPluStorageMethodFkMap : ClassMap<WsSqlPluStorageMethodFkModel>
{
    public WsSqlPluStorageMethodFkMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PlusStorageMethodsFks);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        References(item => item.Plu).Column("PLU_UID").Not.Nullable();
        References(item => item.Method).Column("METHOD_UID").Not.Nullable();
        References(item => item.Resource).Column("RESOURCE_UID").Not.Nullable();
    }
}