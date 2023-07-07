// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Organizations;

/// <summary>
/// Table "ORGANIZATIONS".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlOrganizationModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual int Gln { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlOrganizationModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Gln = 0;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlOrganizationModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Gln = info.GetInt32(nameof(Gln));
    }

    public WsSqlOrganizationModel(WsSqlOrganizationModel item) : base(item)
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
        return Equals((WsSqlOrganizationModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Gln, 0);

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Gln), Gln);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Gln = 1;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlOrganizationModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Gln, item.Gln);

    #endregion
}