namespace WsStorageCore.Entities.SchemaScale.Access;

[DebuggerDisplay("{ToString()}")]
public class WsSqlAccessEntity : WsSqlEntityBase
{
    #region Public and private fields, properties, constructor

    public virtual DateTime LoginDt { get; set; }
    public virtual byte Rights { get; set; }
    public virtual WsEnumAccessRights RightsEnum => (WsEnumAccessRights)Rights;
    
    public WsSqlAccessEntity() : base(WsSqlEnumFieldIdentity.Uid)
    {
        LoginDt = DateTime.MinValue;
        Rights = 0x00;
    }

    public WsSqlAccessEntity(WsSqlAccessEntity item) : base(item)
    {
        LoginDt = item.LoginDt;
        Rights = item.Rights;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{GetIsMarked()} | {Name} | {RightsEnum}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlAccessEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(LoginDt, DateTime.MinValue) &&
        Equals(Rights, (byte)0x00);

    public override void FillProperties()
    {
        base.FillProperties();
        LoginDt = DateTime.Now;
        Name = "KOLBASA-VS\\";
        Rights = (byte)WsEnumAccessRights.None;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlAccessEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(LoginDt, item.LoginDt) &&
        Equals(Rights, item.Rights);

    #endregion
}
