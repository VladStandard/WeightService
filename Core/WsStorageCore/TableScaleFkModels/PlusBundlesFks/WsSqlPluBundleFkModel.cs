// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusBundlesFks;

/// <summary>
/// Table "PLUS_BUNDLES_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlPluBundleFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual WsSqlBundleModel Bundle { get; set; }
    [XmlElement] public virtual WsSqlPluModel Plu { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPluBundleFkModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Bundle = new();
        Plu = new();

    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlPluBundleFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Plu = (WsSqlPluModel)info.GetValue(nameof(Plu), typeof(WsSqlPluModel));
        Bundle = (WsSqlBundleModel)info.GetValue(nameof(Bundle), typeof(WsSqlBundleModel));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{Bundle} | {Plu}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPluBundleFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Bundle.EqualsDefault() &&
        Plu.EqualsDefault();

    public object Clone()
    {
        WsSqlPluBundleFkModel item = new();
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

    public virtual void UpdateProperties(WsSqlPluBundleFkModel pluBundleFk)
    {
        // Get properties from /api/send_nomenclatures/.
        base.UpdateProperties(pluBundleFk, true);
        
        Plu = pluBundleFk.Plu;
        Bundle = pluBundleFk.Bundle;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPluBundleFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Bundle.Equals(item.Bundle) &&
        Plu.Equals(item.Plu);

    public new virtual WsSqlPluBundleFkModel CloneCast() => (WsSqlPluBundleFkModel)Clone();

    #endregion
}