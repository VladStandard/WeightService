// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Scales;

/// <summary>
/// table map "SCALES".
/// </summary>
public sealed class WsSqlScaleMap : ClassMap<WsSqlScaleModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlScaleMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Scales);
        LazyLoad();
        Id(item => item.IdentityValueId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME").Column("CreateDate").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType("DATETIME").Column("ModifiedDate").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType("BIT").Column("Marked").Not.Nullable().Default("0");
        Map(item => item.Description).CustomSqlType("NVARCHAR").Column("Description").Length(150);
        Map(item => item.DeviceSendTimeout).CustomSqlType("SMALLINT").Column("DeviceSendTimeout");
        Map(item => item.DeviceReceiveTimeout).CustomSqlType("SMALLINT").Column("DeviceReceiveTimeout");
        Map(item => item.DeviceComPort).CustomSqlType("VARCHAR").Column("DeviceComPort").Length(5);
        Map(item => item.ZebraIp).CustomSqlType("VARCHAR").Column("ZebraIP").Length(15);
        Map(item => item.ZebraPort).CustomSqlType("SMALLINT").Column("ZebraPort");
        Map(item => item.IsOrder).CustomSqlType("SMALLINT").Column("UseOrder").Default("0").Nullable();
        Map(item => item.Number).CustomSqlType("INT").Column("NUMBER").Not.Nullable();
        Map(item => item.Counter).CustomSqlType("INT").Column("COUNTER").Not.Nullable();
        Map(item => item.ScaleFactor).CustomSqlType("INT").Column("ScaleFactor").Default("1000");
        Map(item => item.IsShipping).CustomSqlType("BIT").Column("IS_SHIPPING").Not.Nullable().Default("0");
        Map(item => item.ShippingLength).CustomSqlType("INT").Column("SHIPPING_LEN").Not.Nullable().Default("0");
        Map(item => item.IsKneading).CustomSqlType("BIT").Column("IS_KNEADING").Not.Nullable().Default("0");
        References(item => item.PrinterMain).Column("ZebraPrinterId").Nullable();
        References(item => item.PrinterShipping).Column("SHIPPING_PRINTER_ID").Nullable();
        References(item => item.WorkShop).Column("WorkShopId").Nullable();
    }
}