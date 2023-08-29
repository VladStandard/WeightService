namespace WsStorageCore.Tables.TableRefModels.ProductionSites;

/// <summary>
/// Table "ProductionSite".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlProductionSiteModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual string Address { get; set; }
    
    public WsSqlProductionSiteModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Address = string.Empty;
    }
    
    protected WsSqlProductionSiteModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Address = info.GetString(nameof(Address));
    }

    public WsSqlProductionSiteModel(WsSqlProductionSiteModel item) : base(item)
    {
        Address = item.Address;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{GetIsMarked()} | {Address}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlProductionSiteModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Address, string.Empty);

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Address), Address);
    }

    public override void FillProperties()
    {
        base.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlProductionSiteModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Address, item.Address);

    #endregion
}