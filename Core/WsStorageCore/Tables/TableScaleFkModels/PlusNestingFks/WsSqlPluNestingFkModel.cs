namespace WsStorageCore.Tables.TableScaleFkModels.PlusNestingFks;

/// <summary>
/// Доменная модель таблицы PLUS_NESTING_FK.
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlPluNestingFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor
    [XmlElement] public virtual WsSqlBoxModel Box { get; set; }
    [XmlElement] public virtual WsSqlPluBundleFkModel PluBundle { get; set; }
    [XmlElement] public virtual bool IsDefault { get; set; }
    [XmlElement] public virtual short BundleCount { get; set; }
    [XmlElement] public virtual decimal WeightMax { get; set; }
    [XmlElement] public virtual decimal WeightMin { get; set; }
    [XmlElement] public virtual decimal WeightNom { get; set; }
    [XmlIgnore] public override string Name => $"{PluBundle.Bundle.Name} | {Box.Name}";
    [XmlElement] public virtual decimal WeightTare { get => PluBundle.Bundle.Weight * BundleCount + Box.Weight; set => _ = value; }
    [XmlIgnore] public virtual string WeightTareKg => $"{WeightTare} {WsLocaleCore.LabelPrint.WeightUnitKg}";
    
    public WsSqlPluNestingFkModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Box = new();
        //Plu = new();
        PluBundle = new();
        IsDefault = false;
        BundleCount = 0;
        WeightMax = 0;
        WeightMin = 0;
        WeightNom = 0;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlPluNestingFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Box = (WsSqlBoxModel)info.GetValue(nameof(Box), typeof(WsSqlBoxModel));
        //Plu = (PluModel)info.GetValue(nameof(Plu), typeof(PluModel));
        PluBundle = (WsSqlPluBundleFkModel)info.GetValue(nameof(PluBundle), typeof(WsSqlPluBundleFkModel));
        IsDefault = info.GetBoolean(nameof(IsDefault));
        BundleCount = info.GetInt16(nameof(BundleCount));
        WeightMax = info.GetDecimal(nameof(WeightMax));
        WeightMin = info.GetDecimal(nameof(WeightMin));
        WeightNom = info.GetDecimal(nameof(WeightNom));
        WeightTare = info.GetDecimal(nameof(WeightTare));
    }

    public WsSqlPluNestingFkModel(WsSqlPluNestingFkModel item) : base(item)
    {
        Box = new(item.Box);
        PluBundle = new(item.PluBundle);
        IsDefault = item.IsDefault;
        BundleCount = item.BundleCount;
        WeightMax = item.WeightMax;
        WeightMin = item.WeightMin;
        WeightNom = item.WeightNom;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | {GetIsDefault()} | {PluBundle.Plu.Number} | {PluBundle.Plu.Name} | " +
        $"{PluBundle.Bundle.Weight} * {BundleCount} + {Box.Weight} = {WeightTare}";
        //$" | {PluBundle.Bundle.Name} * {BundleCount} + {Box.Name}";

    protected virtual string GetIsDefault() => IsDefault ? "Is default" : "No default";


    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPluNestingFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Box.EqualsDefault() &&
        //Plu.EqualsDefault() &&
        PluBundle.EqualsDefault() &&
        Equals(IsDefault, false) &&
        Equals(WeightMax, default(decimal)) &&
        Equals(WeightMin, default(decimal)) &&
        Equals(WeightNom, default(decimal)) &&
        Equals(BundleCount, default(short));

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Box), Box);
        //info.AddValue(nameof(Plu), Plu);
        info.AddValue(nameof(PluBundle), PluBundle);
        info.AddValue(nameof(IsDefault), IsDefault);
        info.AddValue(nameof(BundleCount), BundleCount);
        info.AddValue(nameof(WeightMax), WeightMax);
        info.AddValue(nameof(WeightMin), WeightMin);
        info.AddValue(nameof(WeightNom), WeightNom);
        info.AddValue(nameof(WeightTare), WeightTare);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Box.FillProperties();
        //Plu.FillProperties();
        PluBundle.FillProperties();
        BundleCount = 0;
    }

    public virtual void UpdateProperties(WsSqlPluNestingFkModel item)
    {
        // Get properties from /api/send_nomenclatures/.
        base.UpdateProperties(item, true);
        
        IsDefault = item.IsDefault;
        PluBundle = new(item.PluBundle);
        Box = new(item.Box);
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPluNestingFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Box.Equals(item.Box) &&
        //Plu.Equals(item.Plu) && 
        PluBundle.Equals(item.PluBundle) && 
        Equals(IsDefault, item.IsDefault) &&
        Equals(WeightMax, item.WeightMax) &&
        Equals(WeightMin, item.WeightMin) &&
        Equals(WeightNom, item.WeightNom) &&
        Equals(BundleCount, item.BundleCount);
    
    #endregion
}