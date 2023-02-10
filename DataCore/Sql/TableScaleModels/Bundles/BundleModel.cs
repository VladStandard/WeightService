// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Enums;
using DataCore.Sql.Core.Interfaces;
using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels.Plus;

namespace DataCore.Sql.TableScaleModels.Bundles;

/// <summary>
/// Table "BUNDLES".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(BundleModel)} | {nameof(Uid1C)} = {Uid1C} |  {Name} | {Weight}")]
public class BundleModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual decimal Weight { get; set; }
    [XmlIgnore] public virtual Guid Uid1C { get; set; }

    public BundleModel() : base(SqlFieldIdentity.Uid)
    {
       Weight = 0;
       Uid1C = Guid.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected BundleModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Weight = info.GetDecimal(nameof(Weight));
        Uid1C = info.GetValue(nameof(Uid1C), typeof(Guid)) is Guid uid1C ? uid1C : Guid.Empty;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Weight)}: {Weight}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((BundleModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public new virtual bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Weight, (decimal)0) &&
        Equals(Uid1C, Guid.Empty);

    public override object Clone()
    {
        BundleModel item = new();
        item.CloneSetup(base.CloneCast());
        item.Weight = Weight;
        item.Uid1C = Uid1C;
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
        info.AddValue(nameof(Uid1C), Uid1C);
    }

    public override void UpdateProperties(ISqlTable item)
    {
        base.UpdateProperties(item);
        if (item is not PluModel plu) return;
        Uid1C = plu.PackageTypeGuid;
        Name = plu.PackageTypeName;
        Weight = plu.PackageTypeWeight;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(BundleModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Weight, item.Weight);
    public new virtual BundleModel CloneCast() => (BundleModel)Clone();

    #endregion
}

