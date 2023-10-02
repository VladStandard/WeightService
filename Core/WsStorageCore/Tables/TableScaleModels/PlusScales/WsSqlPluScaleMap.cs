using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableScaleModels.PlusScales;

/// <summary>
/// Table map "PLUS_SCALES".
/// </summary>
public sealed class WsSqlPluScaleMap : ClassMap<WsSqlPluScaleModel>
{
    public WsSqlPluScaleMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PlusScales);
        Not.LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.IsActive).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_ACTIVE").Not.Nullable().Default("0");
        References(item => item.Plu).Column("PLU_UID").Not.Nullable();
        References(item => item.Line).Column("SCALE_ID").Not.Nullable();
    }
}
