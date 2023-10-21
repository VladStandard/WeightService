namespace WsStorageCore.Tables.TableScaleModels.Printers;

[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlPrinterModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    public virtual string Ip { get; set; }
    public virtual short Port { get; set; }
    public virtual string Password { get; set; }
    public virtual WsSqlPrinterTypeModel PrinterType { get; set; }
    public virtual WsSqlFieldMacAddressModel MacAddress { get; set; }
    public virtual string MacAddressValue { get => MacAddress.Value; set => MacAddress.Value = value; }
    public virtual bool PeelOffSet { get; set; }
    public virtual short DarknessLevel { get; set; }
    public virtual string Link => string.IsNullOrEmpty(Ip) ? string.Empty : $"http://{Ip}";
    public override string DisplayName => IsNew ?  WsLocaleCore.Table.FieldEmpty : $"{Name} | {Ip}";

    public WsSqlPrinterModel() : base(WsSqlEnumFieldIdentity.Id)
    {
        Ip = string.Empty;
        Port = 0;
        Password = string.Empty;
        PrinterType = new();
        MacAddress = new();
        PeelOffSet = false;
        DarknessLevel = 0;
    }

    public WsSqlPrinterModel(WsSqlPrinterModel item) : base(item)
    {
        Ip = item.Ip;
        Port = item.Port;
        Password = item.Password;
        PrinterType = new(item.PrinterType);
        MacAddress = new(item.MacAddress);
        PeelOffSet = item.PeelOffSet;
        DarknessLevel = item.DarknessLevel;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(PrinterType)}: {PrinterType}. " +
        $"{nameof(MacAddress)}: {MacAddress}. " +
        $"{nameof(PeelOffSet)}: {PeelOffSet}. " +
        $"{nameof(DarknessLevel)}: {DarknessLevel}.";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPrinterModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Ip, string.Empty) &&
        Equals(Port, (short)0) &&
        Equals(Password, string.Empty) &&
        Equals(PeelOffSet, false) &&
        Equals(DarknessLevel, (short)0) &&
        PrinterType.EqualsDefault() &&
        MacAddress.EqualsDefault();

    public override void FillProperties()
    {
        base.FillProperties();
        DarknessLevel = 1;
        Port = 9100;
        PrinterType.FillProperties();
        MacAddress.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPrinterModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Ip, item.Ip) &&
        Equals(Port, item.Port) &&
        Equals(Password, item.Password) &&
        Equals(PeelOffSet, item.PeelOffSet) &&
        Equals(DarknessLevel, item.DarknessLevel) &&
        PrinterType.Equals(item.PrinterType) &&
        MacAddress.Equals(item.MacAddress);

    #endregion
}
