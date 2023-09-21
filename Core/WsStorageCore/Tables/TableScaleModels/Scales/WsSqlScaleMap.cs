namespace WsStorageCore.Tables.TableScaleModels.Scales;

public sealed class WsSqlScaleMap : ClassMap<WsSqlScaleModel>
{
    public WsSqlScaleMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Scales);
        Not.LazyLoad();
        Id(item => item.IdentityValueId).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CreateDate").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("ModifiedDate").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("Marked").Not.Nullable().Default("0");
        Map(item => item.Description).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("Description").Length(150);
        Map(item => item.DeviceSendTimeout).CustomSqlType(WsSqlFieldTypeUtils.SmallInt).Column("DeviceSendTimeout");
        Map(item => item.DeviceReceiveTimeout).CustomSqlType(WsSqlFieldTypeUtils.SmallInt).Column("DeviceReceiveTimeout");
        Map(item => item.DeviceComPort).CustomSqlType(WsSqlFieldTypeUtils.VarChar).Column("DeviceComPort").Length(5);
        Map(item => item.ZebraIp).CustomSqlType(WsSqlFieldTypeUtils.VarChar).Column("ZebraIP").Length(15);
        Map(item => item.ZebraPort).CustomSqlType(WsSqlFieldTypeUtils.SmallInt).Column("ZebraPort");
        Map(item => item.IsOrder).CustomSqlType(WsSqlFieldTypeUtils.SmallInt).Column("UseOrder").Default("0").Nullable();
        Map(item => item.Number).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("NUMBER").Not.Nullable();
        Map(item => item.LabelCounter).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("COUNTER").Not.Nullable();
        Map(item => item.ScaleFactor).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("ScaleFactor").Default("1000");
        Map(item => item.IsShipping).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_SHIPPING").Not.Nullable().Default("0");
        Map(item => item.ShippingLength).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("SHIPPING_LEN").Not.Nullable().Default("0");
        Map(item => item.IsKneading).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_KNEADING").Not.Nullable().Default("1");
        Map(item => item.ClickOnce).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("CLICK_ONCE").Not.Nullable().Default("");
        References(item => item.PrinterMain).Column("ZebraPrinterId").Nullable();
        References(item => item.PrinterShipping).Column("SHIPPING_PRINTER_ID").Nullable();
        References(item => item.WorkShop).Column("WORKSHOP_UID").Not.Nullable();
        References(item => item.Device).Column("DEVICE_UID").Not.Nullable();
    }
}