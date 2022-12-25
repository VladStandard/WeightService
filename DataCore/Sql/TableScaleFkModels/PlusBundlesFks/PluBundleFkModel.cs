// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleFkModels.BundlesFks;
using DataCore.Sql.TableScaleModels.Plus;

namespace DataCore.Sql.TableScaleFkModels.PlusBundlesFks;

/// <summary>
/// Table "PLUS_BUNDLES_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("Type = {nameof(PluBundleFkModel)}")]
public class PluBundleFkModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual BundleFkModel BundleFk { get; set; }
    [XmlElement] public virtual PluModel Plu { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluBundleFkModel() : base(SqlFieldIdentityEnum.Uid)
    {
        BundleFk = new(); 
        Plu = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PluBundleFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Plu = (PluModel)info.GetValue(nameof(Plu), typeof(PluModel));
        BundleFk = (BundleFkModel)info.GetValue(nameof(BundleFk), typeof(BundleFkModel));
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
        $"{nameof(BundleFk)}: {BundleFk}. " +
        $"{nameof(Plu)}: {Plu}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluBundleFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        BundleFk.EqualsDefault() &&
        Plu.EqualsDefault();

    public override object Clone()
    {
        PluBundleFkModel item = new();
        item.BundleFk = BundleFk.CloneCast();
        item.Plu = Plu.CloneCast();
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
        info.AddValue(nameof(BundleFk), BundleFk);
        info.AddValue(nameof(Plu), Plu);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        BundleFk.FillProperties();
        Plu.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(PluBundleFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        BundleFk.Equals(item.BundleFk) &&
        Plu.Equals(item.Plu);

    public new virtual PluBundleFkModel CloneCast() => (PluBundleFkModel)Clone();

    #endregion
}