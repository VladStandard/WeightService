namespace WsStorageCore.Tables.TableScaleModels.Versions;

/// <summary>
/// Table "VERSIONS".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlVersionModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    public virtual DateTime ReleaseDt { get; set; }
    public virtual short Version { get; set; }
    
    public WsSqlVersionModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        ReleaseDt = DateTime.MinValue;
        Version = 0;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlVersionModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        ReleaseDt = info.GetDateTime(nameof(ReleaseDt));
        Version = info.GetInt16(nameof(Version));
    }

    public WsSqlVersionModel(WsSqlVersionModel item) : base(item)
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
        return Equals((WsSqlVersionModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(ReleaseDt, DateTime.MinValue) &&
        Equals(Version, (short)0);

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(ReleaseDt), ReleaseDt);
        info.AddValue(nameof(Version), Version);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Version = 1;
        ReleaseDt = DateTime.Now;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlVersionModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(ReleaseDt, item.ReleaseDt) &&
        Equals(Version, item.Version);

    #endregion
}