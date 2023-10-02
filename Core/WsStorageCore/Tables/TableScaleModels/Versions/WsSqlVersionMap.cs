using WsStorageCore.OrmUtils;
namespace WsStorageCore.Tables.TableScaleModels.Versions;

/// <summary>
/// Table map "VERSIONS".
/// </summary>
public sealed class WsSqlVersionMap : ClassMap<WsSqlVersionModel>
{
    public WsSqlVersionMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Versions);
        Not.LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.ReleaseDt).CustomSqlType(WsSqlFieldTypeUtils.Date).Column("RELEASE_DT").Not.Nullable();
        Map(item => item.Name).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("NAME").Length(256).Not.Nullable();
        Map(item => item.Description).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("DESCRIPTION").Length(256).Not.Nullable().Default("");
        Map(item => item.Version).CustomSqlType(WsSqlFieldTypeUtils.SmallInt).Column("VERSION").Not.Nullable();
    }
}
