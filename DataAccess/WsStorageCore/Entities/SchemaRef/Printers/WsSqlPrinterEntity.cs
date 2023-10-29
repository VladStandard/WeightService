using WsStorageCore.Enums;
namespace WsStorageCore.Entities.SchemaRef.Printers;

[DebuggerDisplay("{ToString()}")]
public class WsSqlPrinterEntity : WsSqlEntityBase
{
    #region Public and private fields, properties, constructor
    public virtual string Ip { get; set; }
    public virtual short Port { get; set; }
    public virtual PrinterTypeEnum Type { get; set; }
    public virtual string MacAddress { get; set; }
    public virtual string Link => string.IsNullOrEmpty(Ip) ? string.Empty : $"http://{Ip}";
    public override string DisplayName => IsNew ?  WsLocaleCore.Table.FieldEmpty : $"{Name} | {Ip}";

    public WsSqlPrinterEntity() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Ip = string.Empty;
        Port = 0;
        Type = PrinterTypeEnum.Tsc;
        MacAddress = string.Empty;
    }

    public WsSqlPrinterEntity(WsSqlPrinterEntity item) : base(item)
    {
        Ip = item.Ip;
        Port = item.Port;
        Type = item.Type;
        MacAddress = item.MacAddress;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Type)}: {Type}. " +
        $"{nameof(MacAddress)}: {MacAddress}.";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPrinterEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Ip, string.Empty) &&
        Equals(Port, (short)0) &&
        Equals(Type, PrinterTypeEnum.Tsc) &&
        Equals(MacAddress, string.Empty);
         

    public override void FillProperties()
    {
        base.FillProperties();
        Port = 9100;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPrinterEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Ip, item.Ip) &&
        Equals(Port, item.Port) &&
        Equals(Type, PrinterTypeEnum.Tsc) &&
        Equals(MacAddress, item.MacAddress);

    #endregion
}