// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com


using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleModels.Boxes;

namespace DataCore.Sql.TableScaleFkModels.PlusNestingFks;

/// <summary>
/// Table "PLUS_NESTING_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(PluNestingFkModel)}")]
public class PluNestingFkModel : SqlTableBase
{
    #region Public and private fields, properties, constructor
    [XmlElement] public virtual BoxModel Box { get; set; }
    [XmlElement] public virtual PluBundleFkModel PluBundle { get; set; }
    [XmlElement] public virtual bool IsDefault { get; set; }
    [XmlElement] public virtual short BundleCount { get; set; }
    [XmlElement] public virtual decimal WeightMax { get; set; }
    [XmlElement] public virtual decimal WeightMin { get; set; }
    [XmlElement] public virtual decimal WeightNom { get; set; }
    [XmlIgnore] public override string Name => $"{LocaleCore.Scales.Bundle} {BundleCount} {LocaleCore.Scales.WeightUnitGr} | {Box.Name}";
    [XmlIgnore] public virtual decimal WeightTare { get => PluBundle.Bundle.Weight * BundleCount + Box.Weight; set => _ = value; }
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluNestingFkModel() : base(SqlFieldIdentityEnum.Uid)
    {
        PluBundle = new();
        IsDefault = false;
        BundleCount = 0;
        WeightMax = 0;
        WeightMin = 0;
        WeightNom = 0;
        Box = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PluNestingFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        PluBundle = (PluBundleFkModel)info.GetValue(nameof(PluBundle), typeof(PluBundleFkModel));
        IsDefault = info.GetBoolean(nameof(IsDefault));
        BundleCount = info.GetInt16(nameof(BundleCount));
        Box = (BoxModel)info.GetValue(nameof(Box), typeof(BoxModel));
        WeightMax = info.GetDecimal(nameof(WeightMax));
        WeightMin = info.GetDecimal(nameof(WeightMin));
        WeightNom = info.GetDecimal(nameof(WeightNom));
    }

    #endregion

    #region Public and private methods - override

    /// <summary>
    /// To string.
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(PluBundle)}: {PluBundle.Plu.Name}. " +
        $"{nameof(PluBundle)}: {PluBundle.Bundle.Name}. " +
        $"{nameof(WeightTare)}: {WeightTare}. " +
        $"{nameof(IsDefault)}: {IsDefault}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluNestingFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(IsDefault, false) &&
        Equals(WeightMax, default(decimal)) &&
        Equals(WeightMin, default(decimal)) &&
        Equals(WeightNom, default(decimal)) &&
        Equals(BundleCount, default(short)) &&
        Box.EqualsDefault() &&
        PluBundle.EqualsDefault();

    public override object Clone()
    {
        PluNestingFkModel item = new();
        item.IsDefault = IsDefault;
        item.BundleCount = BundleCount;
        item.WeightMax = WeightMax;
        item.WeightMin = WeightMin;
        item.WeightNom = WeightNom;
        item.Box = Box.CloneCast();
        item.PluBundle = PluBundle.CloneCast();
        item.CloneSetup(base.CloneCast());
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
        info.AddValue(nameof(IsDefault), IsDefault);
        info.AddValue(nameof(BundleCount), BundleCount);
        info.AddValue(nameof(Box), Box);
        info.AddValue(nameof(WeightMax), WeightMax);
        info.AddValue(nameof(WeightMin), WeightMin);
        info.AddValue(nameof(WeightNom), WeightNom);
        info.AddValue(nameof(PluBundle), PluBundle);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Box.FillProperties();
        BundleCount = 0;
        PluBundle.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(PluNestingFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Box.Equals(item.Box) &&
        PluBundle.Equals(item.PluBundle) && 
        Equals(IsDefault, item.IsDefault) &&
        Equals(WeightMax, item.WeightMax) &&
        Equals(WeightMin, item.WeightMin) &&
        Equals(WeightNom, item.WeightNom) &&
        Equals(BundleCount, item.BundleCount);
    

    public new virtual PluNestingFkModel CloneCast() => (PluNestingFkModel)Clone();

    #endregion
}