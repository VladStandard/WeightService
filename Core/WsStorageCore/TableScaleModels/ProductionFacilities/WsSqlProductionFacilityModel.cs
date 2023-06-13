// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MissingXmlDoc

namespace WsStorageCore.TableScaleModels.ProductionFacilities;

/// <summary>
/// Table "ProductionFacility".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlProductionFacilityModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual string Address { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlProductionFacilityModel() : base(WsSqlFieldIdentity.Id)
    {
        Address = string.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlProductionFacilityModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Address = info.GetString(nameof(Address));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{Address}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlProductionFacilityModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Address, string.Empty);

    public override object Clone()
    {
        WsSqlProductionFacilityModel item = new();
        item.CloneSetup(base.CloneCast());
        item.Address = Address;
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
        info.AddValue(nameof(Address), Address);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Address = WsLocaleCore.Sql.SqlItemFieldAddress;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlProductionFacilityModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Address, item.Address);

    public new virtual WsSqlProductionFacilityModel CloneCast() => (WsSqlProductionFacilityModel)Clone();

    #endregion
}