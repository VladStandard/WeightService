// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels.Boxes;
using DataCore.Sql.TableScaleModels.Bundles;


namespace DataCore.Sql.TableScaleFkModels.BundlesFks;

/// <summary>
/// Table "BUNDLES_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("Type = {nameof(BundleFkModel)}")]
public class BundleFkModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual short BundleCount { get; set; }
    [XmlElement] public virtual BundleModel Bundle { get; set; }
    [XmlElement] public virtual BoxModel Box { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public BundleFkModel() : base(SqlFieldIdentityEnum.Uid)
    {
        BundleCount = 0;
        Bundle = new();
        Box = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected BundleFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        BundleCount = info.GetInt16(nameof(BundleCount));
        Bundle = (BundleModel)info.GetValue(nameof(Bundle), typeof(BundleModel));
        Box = (BoxModel)info.GetValue(nameof(Box), typeof(BoxModel));
    }

    #endregion

    #region Public and private methods - override

    /// <summary>
    /// To string.
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(BundleCount)}: {BundleCount}. " +
        $"{nameof(Bundle)}: {Bundle}. " +
        $"{nameof(Box)}: {Box}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((BundleFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Bundle.EqualsDefault() &&
        Box.EqualsDefault();
        
    public override object Clone()
    {
        BundleFkModel item = new();
        item.Bundle = Bundle.CloneCast();
        item.Box = Box.CloneCast();
        item.BundleCount = BundleCount;
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
        info.AddValue(nameof(Bundle), Bundle);
        info.AddValue(nameof(Box), Box);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Bundle.FillProperties();
        Box.FillProperties();
        BundleCount = 0;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(BundleFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Bundle.Equals(item.Bundle) &&
        Box.Equals(item.Box) && 
        Equals(BundleCount, item.BundleCount);

    public new virtual BundleFkModel CloneCast() => (BundleFkModel)Clone();

    #endregion
}

