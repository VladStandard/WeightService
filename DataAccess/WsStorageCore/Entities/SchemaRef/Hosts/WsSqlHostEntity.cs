namespace WsStorageCore.Entities.SchemaRef.Hosts;

[DebuggerDisplay("{ToString()}")]
public class WsSqlHostEntity : WsSqlEntityBase
{
    #region Public and private fields, properties, constructor

    public virtual DateTime LoginDt { get; set; }
    public virtual string Ip { get; set; }
    public override string DisplayName => IsNew ?  WsLocaleCore.Table.FieldEmpty : $"{Name} | {Ip}";
    
    public WsSqlHostEntity() : base(WsSqlEnumFieldIdentity.Uid)
    {
        LoginDt = DateTime.MinValue;
        Ip = string.Empty;
    }

    public WsSqlHostEntity(WsSqlHostEntity item) : base(item)
    {
        LoginDt = item.LoginDt;
        Ip = item.Ip;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{GetIsMarked()} | {Name}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlHostEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(LoginDt, DateTime.MinValue) &&
        Equals(Ip, string.Empty);
    
    public override void FillProperties()
    {
        base.FillProperties();
        LoginDt = DateTime.Now;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlHostEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(LoginDt, item.LoginDt) &&
        Equals(Ip, item.Ip);
    
    #endregion
}