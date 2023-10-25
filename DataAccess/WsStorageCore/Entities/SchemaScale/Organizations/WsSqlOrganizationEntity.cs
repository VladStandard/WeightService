namespace WsStorageCore.Entities.SchemaScale.Organizations;

[DebuggerDisplay("{ToString()}")]
public class WsSqlOrganizationEntity : WsSqlEntityBase
{
    #region Public and private fields, properties, constructor

    public virtual int Gln { get; set; }
    
    public WsSqlOrganizationEntity() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Gln = 0;
    }
    
    public WsSqlOrganizationEntity(WsSqlOrganizationEntity item) : base(item)
    {
        Gln = item.Gln;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Description)}: {Description}. " +
        $"{nameof(Gln)}: {Gln}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlOrganizationEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Gln, 0);

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlOrganizationEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Gln, item.Gln);

    #endregion
}