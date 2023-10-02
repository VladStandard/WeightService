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
    
    public virtual WsSqlDeviceModel Device { get; set; }
    public virtual WsSqlWorkShopModel WorkShop { get; set; }
    public virtual WsSqlPrinterModel? PrinterMain { get; set; }
    public virtual WsSqlPrinterModel? PrinterShipping { get; set; }
    public virtual byte ShippingLength { get; set; }
    public virtual string DeviceComPort { get; set; } = "";
    public virtual int Number { get; set; }
    public override string DisplayName => IsNew ?  WsLocaleCore.Table.FieldEmpty : $"{Description}";
    
    private int _labelCounter;
    
    public virtual int LabelCounter { get => _labelCounter; set { _labelCounter = value > 1_000_000 ? 1 : value; } }
    public virtual bool IsShipping { get; set; }
    public virtual bool IsOrder { get; set; }
    public virtual bool IsKneading { get; set; } = true;
    public virtual string NumberWithDescription => $"{WsLocaleCore.Table.Number}: {Number} | {Description}";
    public virtual string ClickOnce { get; set; } = "";

    public WsSqlScaleModel() : base(WsSqlEnumFieldIdentity.Id)
    {
        WorkShop = new();
        Device = new();
    }

    public WsSqlScaleModel(WsSqlScaleModel item) : base(item)
    {
        Device = new(item.Device);
        WorkShop = new(item.WorkShop);
        PrinterMain = item.PrinterMain is null ? null : new(item.PrinterMain);
        PrinterShipping = item.PrinterShipping is null ? null : new(item.PrinterShipping);
        IsShipping = item.IsShipping;
        IsKneading = item.IsKneading;
        ShippingLength = item.ShippingLength;
        DeviceComPort = item.DeviceComPort;
        IsOrder = item.IsOrder;
        Number = item.Number;
        LabelCounter = item.LabelCounter;
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
        Equals(DeviceComPort, string.Empty) &&
        Equals(IsOrder, false) &&
        Equals(Number, 0) &&
        Equals(LabelCounter, 0) &&
        Equals(IsShipping, false) &&
        Equals(IsKneading, false) &&
        Equals(ShippingLength, (byte)0) && 
        WorkShop.EqualsDefault() &&
        Device.EqualsDefault() &&
        (PrinterMain is null || PrinterMain.EqualsDefault()) &&
        (PrinterShipping is null || PrinterShipping.EqualsDefault());

    public override void ClearNullProperties()
    {
        if (PrinterMain is not null && PrinterMain.Identity.EqualsDefault())
            PrinterMain = null;
        if (PrinterShipping is not null && PrinterShipping.Identity.EqualsDefault())
            PrinterShipping = null;
    }

    public override void FillProperties()
    {
        base.FillProperties();
        WorkShop.FillProperties();
        PrinterMain?.FillProperties();
        PrinterShipping?.FillProperties();
        Device.FillProperties();
        DeviceComPort = MdSerialPortsUtils.GenerateComPort(6);
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlScaleModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(DeviceComPort, item.DeviceComPort) &&
        Equals(IsOrder, item.IsOrder) &&
        Equals(Number, item.Number) &&
        Equals(LabelCounter, item.LabelCounter) &&
        Equals(IsShipping, item.IsShipping) &&
        Equals(IsKneading, item.IsKneading) &&
        ShippingLength.Equals(item.ShippingLength) &&
        WorkShop.Equals(item.WorkShop) &&
        Device.Equals(item.WorkShop) &&
        (PrinterMain is null && item.PrinterMain is null || PrinterMain is not null &&
            item.PrinterMain is not null && PrinterMain.Equals(item.PrinterMain)) &&
        (PrinterShipping is null && item.PrinterShipping is null || PrinterShipping is not null &&
            item.PrinterShipping is not null && PrinterShipping.Equals(item.PrinterShipping));

    #endregion
}