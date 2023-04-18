// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusBundlesFks;

/// <summary>
/// Table "PLUS_BUNDLES_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(PluBundleFkModel)} | {Plu.Number} | {Plu.Name} | {Bundle.Name} | {Bundle.Weight}")]
public class PluBundleFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual BundleModel Bundle { get; set; }
    [XmlElement] public virtual PluModel Plu { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluBundleFkModel() : base(WsSqlFieldIdentity.Uid)
    {
        Bundle = new();
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
        Bundle = (BundleModel)info.GetValue(nameof(Bundle), typeof(BundleModel));
    }

    #endregion

    #region Public and private methods - override

    /// <summary>
    /// To string.
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Plu)}: {Plu.Name}. " +
        $"{nameof(Bundle)}: {Bundle.Name}. ";

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
        Bundle.EqualsDefault() &&
        Plu.EqualsDefault();

    public override object Clone()
    {
        PluBundleFkModel item = new();
        item.CloneSetup(base.CloneCast());
        item.Bundle = Bundle.CloneCast();
        item.Plu = Plu.CloneCast();
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
        info.AddValue(nameof(Bundle), Bundle);
        info.AddValue(nameof(Plu), Plu);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Bundle.FillProperties();
        Plu.FillProperties();
    }

    public override void UpdateProperties(WsSqlTableBase item)
    {
        base.UpdateProperties(item, true);
        // Get properties from /api/send_nomenclatures/.
        if (item is not PluBundleFkModel pluBundleFk) return;
        Plu = pluBundleFk.Plu;
        Bundle = pluBundleFk.Bundle;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(PluBundleFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Bundle.Equals(item.Bundle) &&
        Plu.Equals(item.Plu);

    public new virtual PluBundleFkModel CloneCast() => (PluBundleFkModel)Clone();

    #endregion
}