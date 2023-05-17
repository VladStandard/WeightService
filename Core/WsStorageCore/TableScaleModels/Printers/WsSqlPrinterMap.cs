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
        Id(item => item.IdentityValueId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME").Column("CreateDate").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType("DATETIME").Column("ModifiedDate").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType("BIT").Column("Marked").Not.Nullable().Default("0");
        Map(item => item.Name).CustomSqlType("NVARCHAR").Column("Name").Length(100).Nullable();
        Map(item => item.Ip).CustomSqlType("VARCHAR").Length(15).Column("IP").Nullable();
        Map(item => item.Port).CustomSqlType("SMALLINT").Column("Port").Nullable();
        Map(item => item.Password).CustomSqlType("VARCHAR").Length(15).Column("Password").Nullable();
        Map(item => item.MacAddressValue).CustomSqlType("VARCHAR").Column("Mac").Length(20).Nullable();
        Map(item => item.PeelOffSet).CustomSqlType("BIT").Column("PeelOffSet").Not.Nullable().Default("0");
        Map(item => item.DarknessLevel).CustomSqlType("SMALLINT").Column("DarknessLevel").Nullable();
        References(item => item.PrinterType).Column("PrinterTypeId").Not.Nullable();
    }
}
