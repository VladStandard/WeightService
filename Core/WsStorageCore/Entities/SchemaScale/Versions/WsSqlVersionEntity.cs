namespace WsStorageCore.Entities.SchemaScale.Versions;

/// <summary>
/// Table "VERSIONS".
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class WsSqlVersionEntity : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    public virtual DateTime ReleaseDt { get; set; }
    public virtual short Version { get; set; }
    
    public WsSqlVersionEntity() : base(WsSqlEnumFieldIdentity.Uid)
    {
        ReleaseDt = DateTime.MinValue;
        Version = 0;
    }
    
    public WsSqlVersionEntity(WsSqlVersionEntity item) : base(item)
    {
        ReleaseDt = item.ReleaseDt;
        Version = item.Version;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{GetIsMarked()} | {ReleaseDt} | {Version}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlVersionEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(ReleaseDt, DateTime.MinValue) &&
        Equals(Version, (short)0);

    public override void FillProperties()
    {
        base.FillProperties();
        Version = 1;
        ReleaseDt = DateTime.Now;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlVersionEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(ReleaseDt, item.ReleaseDt) &&
        Equals(Version, item.Version);

    #endregion
}