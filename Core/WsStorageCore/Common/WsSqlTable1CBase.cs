namespace WsStorageCore.Common;

[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlTable1CBase : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlIgnore] public virtual Guid Uid1C { get; set; }

    public WsSqlTable1CBase() : base()
    {
        Uid1C = Guid.Empty;
    }

    public WsSqlTable1CBase(WsSqlEnumFieldIdentity identityName) : base(identityName)
    {
        Uid1C = Guid.Empty;
    }

    protected WsSqlTable1CBase(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Uid1C = info.GetValue(nameof(Uid1C), typeof(Guid)) is Guid uid1C ? uid1C : Guid.Empty;
    }

    public WsSqlTable1CBase(WsSqlTable1CBase item) : base(item)
    {
        Uid1C = item.Uid1C;
    }

    #endregion

    #region Public and private methods - override

    public virtual bool Equals(WsSqlTable1CBase item) =>
        ReferenceEquals(this, item) || base.Equals(item) && Equals(Uid1C, item.Uid1C);

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlTable1CBase)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Uid1C), Uid1C);
    }

    #endregion

    #region Public and private methods - virtual

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() && Equals(Uid1C, Guid.Empty);

    #endregion
}