// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Boxes;

/// <summary>
/// Table "BOXES".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlBoxModel : WsSqlTable1CBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual decimal Weight { get; set; }

    public WsSqlBoxModel() : base(WsSqlFieldIdentity.Uid)
    {
        Weight = 0;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlBoxModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Weight = info.GetDecimal(nameof(Weight));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{Uid1C} | {Name} | {Weight}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlBoxModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public new virtual bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Weight, (decimal)0);

    public override object Clone()
    {
        WsSqlBoxModel item = new();
        item.CloneSetup(base.CloneCast());
        item.Weight = Weight;
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
        info.AddValue(nameof(Weight), Weight);
    }

    public override void UpdateProperties(WsSqlTable1CBase item)
    {
        base.UpdateProperties(item);
        // Get properties from /api/send_nomenclatures/.
        if (item is not WsSqlPluModel plu) throw new ArgumentException();
        Uid1C = plu.BoxTypeGuid;
        
        Name = plu.BoxTypeName;
        Weight = plu.BoxTypeWeight;
        if (Equals(Weight, default)) throw new ArgumentException(nameof(Weight));
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlBoxModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Weight, item.Weight);

    public new virtual WsSqlBoxModel CloneCast() => (WsSqlBoxModel)Clone();

    #endregion
}