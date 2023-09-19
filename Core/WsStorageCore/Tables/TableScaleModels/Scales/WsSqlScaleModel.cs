using WsDataCore.Protocols;
using WsStorageCore.Tables.TableRefModels.WorkShops;

namespace WsStorageCore.Tables.TableScaleModels.Scales;

/// <summary>
/// Модель таблицы SCALES.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class WsSqlScaleModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    public virtual WsSqlWorkShopModel? WorkShop { get; set; }
    public virtual WsSqlPrinterModel? PrinterMain { get; set; }
    public virtual WsSqlPrinterModel? PrinterShipping { get; set; }
    public virtual byte ShippingLength { get; set; }
    public virtual short? DeviceSendTimeout { get; set; }
    public virtual short? DeviceReceiveTimeout { get; set; }
    public virtual string DeviceComPort { get; set; } = "";
    public virtual string ZebraIp { get; set; } = "";
    public virtual short? ZebraPort { get; set; }
    public virtual int Number { get; set; }
    public override string DisplayName => IsNew ?  WsLocaleCore.Table.FieldEmpty : $"{Description}";
    
    private int _labelCounter;
    
    public virtual int LabelCounter { get => _labelCounter; set { _labelCounter = value > 1_000_000 ? 1 : value; } }
    public virtual int? ScaleFactor { get; set; }
    public virtual bool IsShipping { get; set; }
    public virtual bool IsOrder { get; set; }
    public virtual bool IsKneading { get; set; } = true;
    public virtual string NumberWithDescription => $"{WsLocaleCore.Table.Number}: {Number} | {Description}";
    public virtual string ClickOnce { get; set; } = "";

    public WsSqlScaleModel() : base(WsSqlEnumFieldIdentity.Id) { }

    public WsSqlScaleModel(WsSqlScaleModel item) : base(item)
    {
        WorkShop = item.WorkShop is null ? null : new(item.WorkShop);
        PrinterMain = item.PrinterMain is null ? null : new(item.PrinterMain);
        PrinterShipping = item.PrinterShipping is null ? null : new(item.PrinterShipping);
        IsShipping = item.IsShipping;
        IsKneading = item.IsKneading;
        ShippingLength = item.ShippingLength;
        DeviceSendTimeout = item.DeviceSendTimeout;
        DeviceReceiveTimeout = item.DeviceReceiveTimeout;
        DeviceComPort = item.DeviceComPort;
        ZebraIp = item.ZebraIp;
        ZebraPort = item.ZebraPort;
        IsOrder = item.IsOrder;
        Number = item.Number;
        LabelCounter = item.LabelCounter;
        ScaleFactor = item.ScaleFactor;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{GetIsMarked()} | {IdentityValueId} | {Description}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlScaleModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(DeviceSendTimeout, null) &&
        Equals(DeviceReceiveTimeout, null) &&
        Equals(DeviceComPort, string.Empty) &&
        Equals(ZebraIp, string.Empty) &&
        Equals(ZebraPort, null) &&
        Equals(IsOrder, false) &&
        Equals(Number, 0) &&
        Equals(LabelCounter, 0) &&
        Equals(ScaleFactor, null) &&
        Equals(IsShipping, false) &&
        Equals(IsKneading, false) &&
        Equals(ShippingLength, (byte)0) &&
        (WorkShop is null || WorkShop.EqualsDefault()) &&
        (PrinterMain is null || PrinterMain.EqualsDefault()) &&
        (PrinterShipping is null || PrinterShipping.EqualsDefault());

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(WorkShop), WorkShop);
        info.AddValue(nameof(PrinterMain), PrinterMain);
        info.AddValue(nameof(PrinterShipping), PrinterShipping);
        info.AddValue(nameof(ShippingLength), ShippingLength);
        info.AddValue(nameof(DeviceSendTimeout), DeviceSendTimeout);
        info.AddValue(nameof(DeviceReceiveTimeout), DeviceReceiveTimeout);
        info.AddValue(nameof(DeviceComPort), DeviceComPort);
        info.AddValue(nameof(ZebraIp), ZebraIp);
        info.AddValue(nameof(ZebraPort), ZebraPort);
        info.AddValue(nameof(Number), Number);
        info.AddValue(nameof(LabelCounter), LabelCounter);
        info.AddValue(nameof(ScaleFactor), ScaleFactor);
        info.AddValue(nameof(IsShipping), IsShipping);
        info.AddValue(nameof(IsOrder), IsOrder);
        info.AddValue(nameof(IsKneading), IsKneading);
    }

    public override void ClearNullProperties()
    {
        if (WorkShop is not null && WorkShop.Identity.EqualsDefault())
            WorkShop = null;
        if (PrinterMain is not null && PrinterMain.Identity.EqualsDefault())
            PrinterMain = null;
        if (PrinterShipping is not null && PrinterShipping.Identity.EqualsDefault())
            PrinterShipping = null;
    }

    public override void FillProperties()
    {
        base.FillProperties();
        WorkShop?.FillProperties();
        PrinterMain?.FillProperties();
        PrinterShipping?.FillProperties();
        ScaleFactor = 1000;
        DeviceComPort = MdSerialPortsUtils.GenerateComPort(6);
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlScaleModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(DeviceSendTimeout, item.DeviceSendTimeout) &&
        Equals(DeviceReceiveTimeout, item.DeviceReceiveTimeout) &&
        Equals(DeviceComPort, item.DeviceComPort) &&
        Equals(ZebraIp, item.ZebraIp) &&
        Equals(ZebraPort, item.ZebraPort) &&
        Equals(IsOrder, item.IsOrder) &&
        Equals(Number, item.Number) &&
        Equals(LabelCounter, item.LabelCounter) &&
        Equals(ScaleFactor, item.ScaleFactor) &&
        Equals(IsShipping, item.IsShipping) &&
        Equals(IsKneading, item.IsKneading) &&
        ShippingLength.Equals(item.ShippingLength) &&
        (WorkShop is null && item.WorkShop is null ||
         WorkShop is not null && item.WorkShop is not null && WorkShop.Equals(item.WorkShop)) &&
        (PrinterMain is null && item.PrinterMain is null || PrinterMain is not null &&
            item.PrinterMain is not null && PrinterMain.Equals(item.PrinterMain)) &&
        (PrinterShipping is null && item.PrinterShipping is null || PrinterShipping is not null &&
            item.PrinterShipping is not null && PrinterShipping.Equals(item.PrinterShipping));

    #endregion
}