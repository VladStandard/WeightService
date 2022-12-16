// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels.Devices;

/// <summary>
/// Table "DEVICES".
/// </summary>
[Serializable]
public class DeviceModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual DateTime LoginDt { get; set; }
    [XmlElement] public virtual DateTime LogoutDt { get; set; }
    [XmlElement] public virtual string PrettyName { get; set; }
    [XmlElement] public virtual string Ipv4 { get; set; }
    [XmlElement] public virtual SqlFieldMacAddressModel MacAddress { get; set; }
    [XmlElement]
    public virtual string MacAddressValue
    {
        get => MacAddress.Value;
        set => MacAddress.Value = value;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public DeviceModel() : base(SqlFieldIdentityEnum.Uid)
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
    protected DeviceModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        LoginDt = info.GetDateTime(nameof(LoginDt));
        LogoutDt = info.GetDateTime(nameof(LogoutDt));
        PrettyName = info.GetString(nameof(PrettyName));
        Ipv4 = info.GetString(nameof(Ipv4));
        MacAddress = (SqlFieldMacAddressModel)info.GetValue(nameof(MacAddress), typeof(SqlFieldMacAddressModel));
    }

    #endregion

    #region Public and private methods - override

    /// <summary>
    /// To string.
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((DeviceModel)obj);
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
        DeviceModel item = new();
        item.LoginDt = LoginDt;
        item.LogoutDt = LogoutDt;
        item.PrettyName = PrettyName;
        item.Ipv4 = Ipv4;
        item.MacAddress = MacAddress.CloneCast();
        item.CloneSetup(base.CloneCast());
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

    public virtual bool Equals(DeviceModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(LoginDt, item.LoginDt) &&
        Equals(LogoutDt, item.LogoutDt) &&
        Equals(PrettyName, item.PrettyName) &&
        Equals(Ipv4, item.Ipv4) &&
        MacAddress.Equals(item.MacAddress);

    public new virtual DeviceModel CloneCast() => (DeviceModel)Clone();

    #endregion
}
