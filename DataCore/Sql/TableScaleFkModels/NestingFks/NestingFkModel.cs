// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels.Boxes;


namespace DataCore.Sql.TableScaleFkModels.NestingFks;

/// <summary>
/// Table "BUNDLES_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(NestingFkModel)}")]
public class NestingFkModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual short BundleCount { get; set; }
    [XmlElement] public virtual decimal WeightMax { get; set; }
    [XmlElement] public virtual decimal WeightMin { get; set; }
    [XmlElement] public virtual decimal WeightNom { get; set; }
    [XmlElement] public virtual BoxModel Box { get; set; }

    /// <summary>
	/// Constructor.
	/// </summary>
	public NestingFkModel() : base(SqlFieldIdentityEnum.Uid)
    {
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
    protected NestingFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
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
        $"{nameof(Name)}: {Name}. ";


    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((NestingFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(WeightMax, default(decimal)) &&
        Equals(WeightMin, default(decimal)) &&
        Equals(WeightNom, default(decimal)) &&
        Equals(BundleCount, default(short)) &&
        Box.EqualsDefault();
        
    public override object Clone()
    {
        NestingFkModel item = new();
        item.Box = Box.CloneCast();
        item.BundleCount = BundleCount;
        item.WeightMax = WeightMax;
        item.WeightMin = WeightMin; 
        item.WeightNom = WeightNom;
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
        info.AddValue(nameof(BundleCount), BundleCount);
        info.AddValue(nameof(Box), Box);
        info.AddValue(nameof(WeightMax), WeightMax);
        info.AddValue(nameof(WeightMin), WeightMin);
        info.AddValue(nameof(WeightNom), WeightNom);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Box.FillProperties();
        BundleCount = 0;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(NestingFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Box.Equals(item.Box) &&
        Equals(WeightMax, item.WeightMax) &&
        Equals(WeightMin, item.WeightMin) &&
        Equals(WeightNom, item.WeightNom) &&
        Equals(BundleCount, item.BundleCount);

    public new virtual NestingFkModel CloneCast() => (NestingFkModel)Clone();

    #endregion
}

