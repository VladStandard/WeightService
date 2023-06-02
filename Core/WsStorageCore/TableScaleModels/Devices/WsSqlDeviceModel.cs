// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Devices;

/// <summary>
/// Table "DEVICES".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlDeviceModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual DateTime LoginDt { get; set; }
    [XmlElement] public virtual DateTime LogoutDt { get; set; }
    [XmlElement] public virtual string PrettyName { get; set; }
    [XmlElement] public virtual string Ipv4 { get; set; }
    [XmlElement] public virtual WsSqlFieldMacAddressModel MacAddress { get; set; }
    
    [XmlElement]
    public virtual string MacAddressValue
    {
        get => MacAddress.Value;
        set => MacAddress.Value = value;
    }

    [XmlIgnore] public virtual string DisplayName => $"{Name} | {Ipv4}";
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlDeviceModel() : base(WsSqlFieldIdentity.Uid)
    {
        LoginDt = DateTime.MinValue;
        LogoutDt = DateTime.MinValue;
        PrettyName = string.Empty;
        Ipv4 = string.Empty;
        MacAddress = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlDeviceModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        LoginDt = info.GetDateTime(nameof(LoginDt));
        LogoutDt = info.GetDateTime(nameof(LogoutDt));
        PrettyName = info.GetString(nameof(PrettyName));
        Ipv4 = info.GetString(nameof(Ipv4));
        MacAddress = (WsSqlFieldMacAddressModel)info.GetValue(nameof(MacAddress), typeof(WsSqlFieldMacAddressModel));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Name)}: {Name}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlDeviceModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(LoginDt, DateTime.MinValue) &&
        Equals(LogoutDt, DateTime.MinValue) &&
        Equals(PrettyName, string.Empty) &&
        Equals(Ipv4, string.Empty) &&
        MacAddress.EqualsDefault();

    public override object Clone()
    {
        WsSqlDeviceModel item = new();
        item.CloneSetup(base.CloneCast());
        item.LoginDt = LoginDt;
        item.LogoutDt = LogoutDt;
        item.PrettyName = PrettyName;
        item.Ipv4 = Ipv4;
        item.MacAddress = MacAddress.CloneCast();
        return item;
    }

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(LoginDt), LoginDt);
        info.AddValue(nameof(LogoutDt), LogoutDt);
        info.AddValue(nameof(PrettyName), PrettyName);
        info.AddValue(nameof(Ipv4), Ipv4);
        info.AddValue(nameof(MacAddress), MacAddress);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        LoginDt = DateTime.Now;
        LogoutDt = DateTime.Now;
        PrettyName = LocaleCore.Sql.SqlItemFieldPrettyName;
        Ipv4 = LocaleCore.Sql.SqlItemFieldIp;
        MacAddressValue = LocaleCore.Sql.SqlItemFieldMac;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlDeviceModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(LoginDt, item.LoginDt) &&
        Equals(LogoutDt, item.LogoutDt) &&
        Equals(PrettyName, item.PrettyName) &&
        Equals(Ipv4, item.Ipv4) &&
        MacAddress.Equals(item.MacAddress);

    public new virtual WsSqlDeviceModel CloneCast() => (WsSqlDeviceModel)Clone();

    #endregion
}
