// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Enums;
using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels.PrintersTypes;

namespace DataCore.Sql.TableScaleModels.Printers;

/// <summary>
/// Table "ZebraPrinter".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(PrinterModel)}")]
public class PrinterModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual string Ip { get; set; }
    [XmlElement] public virtual short Port { get; set; }
    [XmlElement] public virtual string Password { get; set; }
    [XmlElement] public virtual PrinterTypeModel PrinterType { get; set; }
    [XmlElement] public virtual SqlFieldMacAddressModel MacAddress { get; set; }
    [XmlElement] public virtual string MacAddressValue { get => MacAddress.Value; set => MacAddress.Value = value; }
    [XmlElement] public virtual bool PeelOffSet { get; set; }
    [XmlElement] public virtual short DarknessLevel { get; set; }
    [XmlIgnore] public virtual string Link => string.IsNullOrEmpty(Ip) ? string.Empty : $"http://{Ip}";
    [XmlIgnore] public virtual HttpStatusCode HttpStatusCode { get; set; }
    [XmlIgnore] public virtual IPStatus PingStatus { get; set; }
    [XmlIgnore] public virtual bool IsPing => PingStatus == IPStatus.Success;
    [XmlIgnore] public virtual Exception? HttpStatusException { get; set; }
    [XmlIgnore] public virtual bool IsConnect => HttpStatusCode == HttpStatusCode.OK;

    /// <summary>
    /// Constructor.
    /// </summary>
    public PrinterModel() : base(SqlFieldIdentity.Id)
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
    protected PrinterModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Ip = info.GetString(nameof(Ip));
        Port = info.GetInt16(nameof(Port));
        Password = info.GetString(nameof(Password));
        PrinterType = (PrinterTypeModel)info.GetValue(nameof(PrinterType), typeof(PrinterTypeModel));
        MacAddress = (SqlFieldMacAddressModel)info.GetValue(nameof(MacAddress), typeof(SqlFieldMacAddressModel));
        PeelOffSet = info.GetBoolean(nameof(PeelOffSet));
        DarknessLevel = info.GetInt16(nameof(DarknessLevel));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
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
        return Equals((PrinterModel)obj);
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

    public override object Clone()
    {
        PrinterModel item = new();
        item.Ip = Ip;
        item.Port = Port;
        item.Password = Password;
        item.PrinterType = PrinterType.CloneCast();
        item.MacAddress = MacAddress.CloneCast();
        item.PeelOffSet = PeelOffSet;
        item.DarknessLevel = DarknessLevel;
        item.HttpStatusCode = HttpStatusCode;
        item.HttpStatusException = HttpStatusException;
        item.CloneSetup(base.CloneCast());
        return item;
    }

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
        PrinterType.FillProperties();
        MacAddress.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(PrinterModel item) =>
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

    public new virtual PrinterModel CloneCast() => (PrinterModel)Clone();

    #endregion
}
