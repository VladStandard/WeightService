// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PrintersResourcesFks;

/// <summary>
/// Table map "ZebraPrinterResourceRef".
/// </summary>
public sealed class WsSqlPrinterResourceFkMap : ClassMap<WsSqlPrinterResourceFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPrinterResourceFkMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PrintersResourcesFks);
        LazyLoad();
        Id(x => x.IdentityValueId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CreateDate").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("ModifiedDate").Not.Nullable();
        References(x => x.Printer).Column("PrinterID").Not.Nullable();
        References(x => x.TemplateResource).Column("RESOURCE_UID").Not.Nullable();
    }
}