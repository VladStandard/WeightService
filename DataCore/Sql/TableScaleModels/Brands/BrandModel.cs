// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Enums;
using DataCore.Sql.Core.Models;

namespace DataCore.Sql.TableScaleModels.Brands;

/// <summary>
/// Table "BRANDS".
/// </summary>
[Serializable]
[XmlRoot("Brand", Namespace = "", IsNullable = false)]
[DebuggerDisplay("{nameof(BrandModel)} | {Code} | {Name} | {Uid1c} ")]
public class BrandModel : SqlTableBase1c
{
    #region Public and private fields, properties, constructor

    [XmlAttribute] public virtual string Code { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public BrandModel() : base(WsSqlFieldIdentity.Uid)
    {
        Code = string.Empty;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected BrandModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Code = info.GetString(nameof(Code));
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
        base.EqualsDefault() && Equals(Code, string.Empty);

    public override object Clone()
    {
        BrandModel item = new();
        item.CloneSetup(base.CloneCast());
        item.Code = Code;
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

    public override void UpdateProperties(SqlTableBase1c item)
    {
        base.UpdateProperties(item);
        // Get properties from /api/send_brands/.
        if (item is not BrandModel brand) throw new ArgumentException(nameof(item));
        Uid1c = brand.Uid1c;
        
        Code = brand.Code;
    }

    #endregion
}