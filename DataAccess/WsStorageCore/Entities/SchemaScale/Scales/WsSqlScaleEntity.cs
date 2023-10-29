using WsDataCore.Protocols;
using WsStorageCore.Entities.SchemaRef.Hosts;
using WsStorageCore.Entities.SchemaRef.Printers;
namespace WsStorageCore.Entities.SchemaScale.Scales;

/// <summary>
/// Модель таблицы SCALES.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class WsSqlScaleEntity : WsSqlEntityBase
{
    #region Public and private fields, properties, constructor
    
    public virtual WsSqlHostEntity Host { get; set; }
    public virtual WsSqlWorkShopEntity WorkShop { get; set; }
    public virtual WsSqlPrinterEntity Printer { get; set; }
    public virtual string DeviceComPort { get; set; } = "";
    public virtual int Number { get; set; }
    public override string DisplayName => IsNew ?  WsLocaleCore.Table.FieldEmpty : $"{Description}";
    
    private int _labelCounter;
    
    public virtual int LabelCounter { get => _labelCounter; set { _labelCounter = value > 1_000_000 ? 1 : value; } }
    public virtual string NumberWithDescription => $"{WsLocaleCore.Table.Number}: {Number} | {Description}";
    public virtual string ClickOnce { get; set; } = "";

    public WsSqlScaleEntity() : base(WsSqlEnumFieldIdentity.Id)
    {
        WorkShop = new();
        Host = new();
        Printer = new();
        Number = 0;
        LabelCounter = 0;
    }

    public WsSqlScaleEntity(WsSqlScaleEntity item) : base(item)
    {
        Host = new(item.Host);
        WorkShop = new(item.WorkShop);
        Printer = new(item.Printer);
        DeviceComPort = item.DeviceComPort;
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
        return Equals((WsSqlScaleEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(DeviceComPort, string.Empty) &&
        Equals(Number, 0) &&
        Equals(LabelCounter, 0) &&
        WorkShop.EqualsDefault() &&
        Host.EqualsDefault() &&
        Printer.EqualsDefault();

    public override void FillProperties()
    {
        base.FillProperties();
        WorkShop.FillProperties();
        Printer.FillProperties();
        Host.FillProperties();
        DeviceComPort = MdSerialPortsUtils.GenerateComPort(6);
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlScaleEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(DeviceComPort, item.DeviceComPort) &&
        Equals(Number, item.Number) &&
        Equals(LabelCounter, item.LabelCounter) &&
        WorkShop.Equals(item.WorkShop) &&
        Host.Equals(item.Host) &&
        Printer.Equals(item.Printer);

    #endregion
}