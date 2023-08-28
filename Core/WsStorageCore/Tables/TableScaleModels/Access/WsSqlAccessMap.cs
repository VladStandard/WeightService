namespace WsStorageCore.Tables.TableScaleModels.Access;

/// <summary>
/// Table map "ACCESS".
/// </summary>
public sealed class WsSqlAccessMap : ClassMap<WsSqlAccessModel>
{
    public WsSqlAccessMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Access);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CHANGE_DT").Not.Nullable();
        Map(item => item.LoginDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("LOGIN_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Name).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("NAME").Length(32).Not.Nullable();
        Map(item => item.Rights).CustomSqlType(WsSqlFieldTypeUtils.TinyInt).Column("RIGHTS").Not.Nullable().Default("0");
    }
}
