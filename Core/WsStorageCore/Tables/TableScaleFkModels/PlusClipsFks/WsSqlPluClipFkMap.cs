namespace WsStorageCore.Tables.TableScaleFkModels.PlusClipsFks;

/// <summary>
/// Маппинг таблицы связей клипс и ПЛУ.
/// </summary>
public sealed class WsSqlPluClipFkMap : ClassMap<WsSqlPluClipFkModel>
{
    public WsSqlPluClipFkMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PlusClipsFks);
        Not.LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        References(item => item.Clip).Column("CLIP_UID").Not.Nullable();
        References(item => item.Plu).Column("PLU_UID").Unique().Not.Nullable();
    }
}