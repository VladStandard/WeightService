namespace WsStorageCore.Tables.TableScaleModels.Devices;

[DebuggerDisplay("{ToString()}")]
public class WsSqlDeviceModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    public virtual DateTime LoginDt { get; set; }
    public virtual DateTime LogoutDt { get; set; }
    public virtual string Ipv4 { get; set; }
    public virtual WsSqlFieldMacAddressModel MacAddress { get; set; } 
    public virtual string MacAddressValue { get => MacAddress.Value; set => MacAddress.Value = value; } 
    public override string DisplayName => IsNew ?  WsLocaleCore.Table.FieldEmpty : $"{Name} | {Ipv4}";
    
    public WsSqlDeviceModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        LoginDt = DateTime.MinValue;
        LogoutDt = DateTime.MinValue;
        Ipv4 = string.Empty;
        MacAddress = new();
    }

    public WsSqlDeviceModel(WsSqlDeviceModel item) : base(item)
    {
        LoginDt = item.LoginDt;
        LogoutDt = item.LogoutDt;
        Ipv4 = item.Ipv4;
        MacAddress = new(item.MacAddress);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{GetIsMarked()} | {Name}";

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
        Equals(Ipv4, string.Empty) &&
        MacAddress.EqualsDefault();
    
    public override void FillProperties()
    {
        base.FillProperties();
        LoginDt = DateTime.Now;
        LogoutDt = DateTime.Now;
        Ipv4 = WsLocaleCore.Sql.SqlItemFieldIp;
        MacAddressValue = WsLocaleCore.Sql.SqlItemFieldMac;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlDeviceModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(LoginDt, item.LoginDt) &&
        Equals(LogoutDt, item.LogoutDt) &&
        Equals(Ipv4, item.Ipv4) &&
        MacAddress.Equals(item.MacAddress);
    
    #endregion
}