// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Clips;

/// <summary>
/// Table "CLIPS".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlClipModel : WsSqlTable1CBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual decimal Weight { get; set; }

    public WsSqlClipModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Weight = 0;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlClipModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Weight = info.GetDecimal(nameof(Weight));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{GetIsMarked()} | {Name} | {Weight}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlClipModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public new virtual bool EqualsDefault() =>
        base.EqualsDefault() && Equals(Weight, (decimal)0);

    public object Clone()
    {
        WsSqlClipModel item = new();
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

    public virtual void UpdateProperties(WsSqlPluModel plu)
    {
        // Get properties from /api/send_nomenclatures/.
        
        Uid1C = plu.ClipTypeGuid;
        Name = plu.ClipTypeName;
        Weight = plu.ClipTypeWeight;
        if (Equals(Weight, default)) throw new ArgumentException(nameof(Weight));
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlClipModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Weight, item.Weight);
    public new virtual WsSqlClipModel CloneCast() => (WsSqlClipModel)Clone();

    #endregion
}