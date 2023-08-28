namespace WsStorageCore.Tables.TableScaleFkModels.PlusTemplatesFks;

public sealed class WsSqlPluTemplateFkMap : ClassMap<WsSqlPluTemplateFkModel>
{
    public WsSqlPluTemplateFkMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PlusTemplatesFks);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        References(item => item.Plu).Column("PLU_UID").Not.Nullable();
        References(item => item.Template).Column("TEMPLATE_ID").Not.Nullable();
    }
}