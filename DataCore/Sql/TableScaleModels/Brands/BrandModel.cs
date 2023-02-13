// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Enums;
using DataCore.Sql.Core.Interfaces;
using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels.Plus;

namespace DataCore.Sql.TableScaleModels.Brands;

/// <summary>
/// Table "BRANDS".
/// </summary>
[Serializable]
[XmlRoot("Brand", Namespace = "", IsNullable = false)]
[DebuggerDisplay("{nameof(BrandModel)} | {nameof(Uid1C)} = {Uid1C} | {Code} | {Name}")]
public class BrandModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlAttribute] public virtual string Code { get; set; }
    [XmlIgnore] public virtual Guid Uid1C { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public BrandModel() : base(SqlFieldIdentity.Uid)
    {
        Code = string.Empty;
        Uid1C = Guid.Empty;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected BrandModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Code = info.GetString(nameof(Code));
        Uid1C = info.GetValue(nameof(Uid1C), typeof(Guid)) is Guid uid1C ? uid1C : Guid.Empty;
    }

    #endregion

    #region Public and private methods - override

    /// <summary>
    /// To string.
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Code)}: {Code}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((BrandModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Code, string.Empty) &&
        Equals(Uid1C, Guid.Empty);

    public override object Clone()
    {
        BrandModel item = new();
        item.CloneSetup(base.CloneCast());
        item.Code = Code;
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
        info.AddValue(nameof(Code), Code);
        info.AddValue(nameof(Uid1C), Uid1C);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Code = LocaleCore.Sql.SqlItemFieldCode;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(BrandModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Code, item.Code);

    public new virtual BrandModel CloneCast() => (BrandModel)Clone();

    public override void UpdateProperties(ISqlTable item)
    {
        base.UpdateProperties(item);
        // Get properties from /api/send_brands/.
        if (item is not BrandModel brand) return;
        Code = brand.Code;
        Uid1C = brand.IdentityValueUid;
    }

    #endregion
}