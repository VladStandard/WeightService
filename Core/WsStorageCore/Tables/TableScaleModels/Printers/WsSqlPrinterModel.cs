// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Net.NetworkInformation;

namespace WsStorageCore.Tables.TableScaleModels.Printers;

[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlPrinterModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual string Ip { get; set; }
    [XmlElement] public virtual short Port { get; set; }
    [XmlElement] public virtual string Password { get; set; }
    [XmlElement] public virtual WsSqlPrinterTypeModel PrinterType { get; set; }
    [XmlElement] public virtual WsSqlFieldMacAddressModel MacAddress { get; set; }
    [XmlElement] public virtual string MacAddressValue { get => MacAddress.Value; set => MacAddress.Value = value; }
    [XmlElement] public virtual bool PeelOffSet { get; set; }
    [XmlElement] public virtual short DarknessLevel { get; set; }
    [XmlIgnore] public virtual string Link => string.IsNullOrEmpty(Ip) ? string.Empty : $"http://{Ip}";
    [XmlIgnore] public virtual HttpStatusCode HttpStatusCode { get; set; }
    [XmlIgnore] public virtual IPStatus PingStatus { get; set; }
    [XmlIgnore] public virtual bool IsPing => PingStatus == IPStatus.Success;
    [XmlIgnore] public virtual Exception? HttpStatusException { get; set; }
    [XmlIgnore] public virtual bool IsConnect => HttpStatusCode == HttpStatusCode.OK;
    [XmlIgnore] public override string DisplayName => IsNew ?  WsLocaleCore.Table.FieldEmpty : $"{Name} | {Ip}";

    public WsSqlPrinterModel() : base(WsSqlEnumFieldIdentity.Id)
    {
        Ip = string.Empty;
        Port = 0;
        Password = string.Empty;
        PrinterType = new();
        MacAddress = new();
        PeelOffSet = false;
        DarknessLevel = 0;
        HttpStatusCode = HttpStatusCode.BadRequest;
        HttpStatusException = null;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlPrinterModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Ip = info.GetString(nameof(Ip));
        Port = info.GetInt16(nameof(Port));
        Password = info.GetString(nameof(Password));
        PrinterType = (WsSqlPrinterTypeModel)info.GetValue(nameof(PrinterType), typeof(WsSqlPrinterTypeModel));
        MacAddress = (WsSqlFieldMacAddressModel)info.GetValue(nameof(MacAddress), typeof(WsSqlFieldMacAddressModel));
        PeelOffSet = info.GetBoolean(nameof(PeelOffSet));
        DarknessLevel = info.GetInt16(nameof(DarknessLevel));
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
        HttpStatusCode = item.HttpStatusCode;
        HttpStatusException = item.HttpStatusException;

    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(PrinterType)}: {PrinterType}. " +
        $"{nameof(MacAddress)}: {MacAddress}. " +
        $"{nameof(PeelOffSet)}: {PeelOffSet}. " +
        $"{nameof(DarknessLevel)}: {DarknessLevel}. " +
        $"{nameof(HttpStatusCode)}: {HttpStatusCode}. " +
        $"{nameof(HttpStatusException)}: {HttpStatusException}. ";

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
        Equals(HttpStatusCode, HttpStatusCode.BadRequest) &&
        Equals(HttpStatusException, null) &&
        PrinterType.EqualsDefault() &&
        MacAddress.EqualsDefault();

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Ip), Ip);
        info.AddValue(nameof(Port), Port);
        info.AddValue(nameof(Password), Password);
        info.AddValue(nameof(PrinterType), PrinterType);
        info.AddValue(nameof(MacAddress), MacAddress);
        info.AddValue(nameof(PeelOffSet), PeelOffSet);
        info.AddValue(nameof(DarknessLevel), DarknessLevel);
    }

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
        Equals(HttpStatusCode, item.HttpStatusCode) &&
        Equals(HttpStatusException, item.HttpStatusException) &&
        PrinterType.Equals(item.PrinterType) &&
        MacAddress.Equals(item.MacAddress);

    #endregion
}
