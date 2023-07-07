// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Bundles;

/// <summary>
/// Table "BUNDLES".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlBundleModel : WsSqlTable1CBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual decimal Weight { get; set; }

    public WsSqlBundleModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
       Weight = 0;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlBundleModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Weight = info.GetDecimal(nameof(Weight));
    }

    public WsSqlBundleModel(WsSqlBundleModel item) : base(item)
    {
        Weight = item.Weight;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{Name} | {Weight} | {Uid1C}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlBundleModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public new virtual bool EqualsDefault() =>
        base.EqualsDefault() && Equals(Weight, (decimal)0);

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Weight), Weight);
    }

    public virtual void UpdateProperties(WsSqlPluModel plu)
    {
        // Get properties from /api/send_nomenclatures/.
        
        Uid1C = plu.PackageTypeGuid;
        Name = plu.PackageTypeName;
        Weight = plu.PackageTypeWeight;
        if (Equals(Weight, default)) throw new ArgumentException(nameof(Weight));
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlBundleModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Weight, item.Weight);
    #endregion
}

