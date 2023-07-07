// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsSqlTableBase = WsStorageCore.Common.WsSqlTableBase;

namespace WsStorageCore.TableScaleFkModels.PlusBrandsFks;

/// <summary>
/// Table "PLUS_BRANDS_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlPluBrandFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual WsSqlPluModel Plu { get; set; }
    [XmlElement] public virtual WsSqlBrandModel Brand { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPluBrandFkModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Plu = new();
        Brand = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlPluBrandFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Plu = (WsSqlPluModel)info.GetValue(nameof(Plu), typeof(WsSqlPluModel));
        Brand = (WsSqlBrandModel)info.GetValue(nameof(Brand), typeof(WsSqlBrandModel));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Plu)}: {Plu.Name}. " +
        $"{nameof(Brand)}: {Brand.Name}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPluBrandFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Brand.EqualsDefault() &&
        Plu.EqualsDefault();

    public object Clone()
    {
        WsSqlPluBrandFkModel item = new();
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

    public virtual void UpdateProperties(WsSqlPluBrandFkModel pluBrandFk)
    {
        // Get properties from /api/send_nomenclatures/.
        base.UpdateProperties(pluBrandFk, true);
        
        Plu = pluBrandFk.Plu;
        Brand = pluBrandFk.Brand;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPluBrandFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Plu.Equals(item.Plu) &&
        Brand.Equals(item.Brand);

    public new virtual WsSqlPluBrandFkModel CloneCast() => (WsSqlPluBrandFkModel)Clone();

    #endregion
}