// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsSqlTableBase = WsStorageCore.Tables.WsSqlTableBase;

namespace WsStorageCore.TableScaleFkModels.PlusBrandsFks;

/// <summary>
/// Table "PLUS_BRANDS_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(PluBrandFkModel)} | {Plu.Number} | {Plu.Name} | {Brand.Name} ")]
public class PluBrandFkModel : Tables.WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual WsSqlPluModel Plu { get; set; }
    [XmlElement] public virtual BrandModel Brand { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluBrandFkModel() : base(WsSqlFieldIdentity.Uid)
    {
        Plu = new();
        Brand = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PluBrandFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Plu = (WsSqlPluModel)info.GetValue(nameof(Plu), typeof(WsSqlPluModel));
        Brand = (BrandModel)info.GetValue(nameof(Brand), typeof(BrandModel));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Plu)}: {Plu.Name}. " +
        $"{nameof(Brand)}: {Brand.Name}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluBrandFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Brand.EqualsDefault() &&
        Plu.EqualsDefault();

    public override object Clone()
    {
        PluBrandFkModel item = new();
        item.CloneSetup(base.CloneCast());
        item.Plu = Plu.CloneCast();
        item.Brand = Brand.CloneCast();
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
        info.AddValue(nameof(Plu), Plu);
        info.AddValue(nameof(Brand), Brand);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Plu.FillProperties();
        Brand.FillProperties();
    }

    public override void UpdateProperties(WsSqlTableBase item)
    {
        base.UpdateProperties(item, true);
        // Get properties from /api/send_nomenclatures/.
        if (item is not PluBrandFkModel pluBundleFk) return;
        Plu = pluBundleFk.Plu;
        Brand = pluBundleFk.Brand;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(PluBrandFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Plu.Equals(item.Plu) &&
        Brand.Equals(item.Brand);

    public new virtual PluBrandFkModel CloneCast() => (PluBrandFkModel)Clone();

    #endregion
}