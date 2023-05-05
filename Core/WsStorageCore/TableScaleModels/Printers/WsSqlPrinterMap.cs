// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Printers;

/// <summary>
/// Table map "ZebraPrinter".
/// </summary>
public sealed class WsSqlPrinterMap : ClassMap<WsSqlPrinterModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPrinterMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Printers);
        LazyLoad();
        Id(x => x.IdentityValueId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CreateDate").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("ModifiedDate").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("Marked").Not.Nullable().Default("0");
        Map(x => x.Name).CustomSqlType("NVARCHAR").Column("Name").Length(100).Nullable();
        Map(x => x.Ip).CustomSqlType("VARCHAR").Length(15).Column("IP").Nullable();
        Map(x => x.Port).CustomSqlType("SMALLINT").Column("Port").Nullable();
        Map(x => x.Password).CustomSqlType("VARCHAR").Length(15).Column("Password").Nullable();
        Map(x => x.MacAddressValue).CustomSqlType("VARCHAR").Column("Mac").Length(20).Nullable();
        Map(x => x.PeelOffSet).CustomSqlType("BIT").Column("PeelOffSet").Not.Nullable().Default("0");
        Map(x => x.DarknessLevel).CustomSqlType("SMALLINT").Column("DarknessLevel").Nullable();
        References(x => x.PrinterType).Column("PrinterTypeId").Not.Nullable();
    }
}
