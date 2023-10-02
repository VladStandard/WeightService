using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableScaleFkModels.PrintersResourcesFks;

public sealed class WsSqlPrinterResourceFkMap : ClassMap<WsSqlPrinterResourceFkModel>
{
    public WsSqlPrinterResourceFkMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PrintersResourcesFks);
        Not.LazyLoad();
        Id(item => item.IdentityValueId).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CreateDate").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("ModifiedDate").Not.Nullable();
        References(item => item.Printer).Column("PrinterID").Not.Nullable();
        References(item => item.TemplateResource).Column("RESOURCE_UID").Not.Nullable();
    }
}