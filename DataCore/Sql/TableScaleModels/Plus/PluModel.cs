// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

using DataCore.Sql.Core.Enums;
using DataCore.Sql.Core.Interfaces;
using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels.Nomenclatures;
using DataCore.Sql.Xml;

namespace DataCore.Sql.TableScaleModels.Plus;

/// <summary>
/// Table "PLUS".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(PluModel)} | {Name} | {nameof(Number)} = {Number} | {nameof(Code)} = {Code}")]
public class PluModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual bool IsGroup { get; set; } = false;
    [XmlIgnore] public virtual Guid ParentGuid { get; set; }
    [XmlElement] public virtual ushort Number { get; set; }
    [XmlElement] public virtual string Code { get; set; }
    [XmlElement] public virtual string NumberFormat
    {
        get => $"{Number:000}";
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement] public virtual string FullName { get; set; }
    [XmlElement] public virtual ushort ShelfLifeDays { get; set; }
    [XmlElement] public virtual string Gtin { get; set; }
    [XmlElement] public virtual string Gtin14Format
    {
        get => Gtin.Length switch
            {
                13 => BarcodeHelper.Instance.GetGtinWithCheckDigit(Gtin[..13]),
                14 => Gtin,
                _ => "ERROR"
            };
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement] public virtual string Ean13 { get; set; }
    [XmlElement] public virtual string Itf14 { get; set; }
    [XmlElement] public virtual bool IsCheckWeight { get; set; }
    [XmlElement] public virtual NomenclatureModel Nomenclature { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluModel() : base(SqlFieldIdentity.Uid)
    {
        IsGroup = default;
        ParentGuid = Guid.Empty;
        Number = default;
        FullName = string.Empty;
        ShelfLifeDays = default;
        Gtin = string.Empty;
        Ean13 = string.Empty;
        Itf14 = string.Empty;
        Code = string.Empty;
        IsCheckWeight = false;
        Nomenclature = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PluModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        IsGroup = info.GetBoolean(nameof(IsGroup));
        object parentGroupGuid = info.GetValue(nameof(ParentGuid), typeof(Guid));
        ParentGuid = parentGroupGuid is Guid guid ? guid : Guid.Empty;
        Number = info.GetUInt16(nameof(Number));
        FullName = info.GetString(nameof(FullName));
        ShelfLifeDays = info.GetUInt16(nameof(ShelfLifeDays));
        Gtin = info.GetString(nameof(Gtin));
        Ean13 = info.GetString(nameof(Ean13));
        Itf14 = info.GetString(nameof(Itf14));
        Code = info.GetString(nameof(Code));
        IsCheckWeight = info.GetBoolean(nameof(IsCheckWeight));
        Nomenclature = (NomenclatureModel)info.GetValue(nameof(Nomenclature), typeof(NomenclatureModel));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(IsGroup)}: {IsGroup}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Code)}: {Code}. " +
        $"{nameof(Number)}: {Number}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(IsGroup, default) &&
        Equals(ParentGuid, Guid.Empty) &&
        Equals(Code, string.Empty) &&
        Equals(Number, default(ushort)) &&
        Equals(FullName, string.Empty) &&
        Equals(ShelfLifeDays, default(ushort)) &&
        Equals(Gtin, string.Empty) &&
        Equals(Ean13, string.Empty) &&
        Equals(Itf14, string.Empty) &&
        Equals(IsCheckWeight, false) &&
        Nomenclature.EqualsDefault();

    public override object Clone()
    {
        PluModel item = new();
        item.IsGroup = IsGroup;
        item.ParentGuid = ParentGuid;
        item.Code = Code;
        item.Number = Number;
        item.FullName = FullName;
        item.ShelfLifeDays = ShelfLifeDays;
        item.Gtin = Gtin;
        item.Ean13 = Ean13;
        item.Itf14 = Itf14;
        item.IsCheckWeight = IsCheckWeight;
        item.Nomenclature = Nomenclature.CloneCast();
        item.CloneSetup(base.CloneCast());
        return item;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(IsGroup), IsGroup);
        info.AddValue(nameof(ParentGuid), ParentGuid);
        info.AddValue(nameof(Number), Number);
        info.AddValue(nameof(FullName), FullName);
        info.AddValue(nameof(ShelfLifeDays), ShelfLifeDays);
        info.AddValue(nameof(Gtin), Gtin);
        info.AddValue(nameof(Ean13), Ean13);
        info.AddValue(nameof(Itf14), Itf14);
        info.AddValue(nameof(IsCheckWeight), IsCheckWeight);
        info.AddValue(nameof(Nomenclature), Nomenclature);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Code = LocaleCore.Sql.SqlItemFieldCode;
        Number = 100;
        FullName = LocaleCore.Sql.SqlItemFieldFullName;
        Gtin = LocaleCore.Sql.SqlItemFieldGtin;
        Ean13 = LocaleCore.Sql.SqlItemFieldEan13;
        Itf14 = LocaleCore.Sql.SqlItemFieldItf14;
        Nomenclature.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(PluModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(IsGroup, item.IsGroup) &&
        Equals(ParentGuid, item.ParentGuid) &&
        Equals(Code, item.Code) &&
        Equals(Number, item.Number) &&
        Equals(FullName, item.FullName) &&
        Equals(ShelfLifeDays, item.ShelfLifeDays) &&
        Equals(Gtin, item.Gtin) &&
        Equals(Ean13, item.Ean13) &&
        Equals(Itf14, item.Itf14) &&
        Equals(IsCheckWeight, item.IsCheckWeight) &&
        Nomenclature.Equals(item.Nomenclature);

    public new virtual PluModel CloneCast() => (PluModel)Clone();

    public override void UpdateProperties(ISqlTable item)
    {
        base.UpdateProperties(item);
        if (item is not PluModel plu) return;

        IsGroup = plu.IsGroup;
        if (plu.Number > 0)
            Number = plu.Number;
        if (!string.IsNullOrEmpty(plu.Code))
            Code = plu.Code;
        if (!string.IsNullOrEmpty(plu.FullName))
            FullName = plu.FullName;
        if (plu.ShelfLifeDays > 0)
            ShelfLifeDays = plu.ShelfLifeDays;
        if (!string.IsNullOrEmpty(plu.Gtin))
            Gtin = plu.Gtin;
        if (!string.IsNullOrEmpty(plu.Ean13))
            Ean13 = plu.Ean13;
        if (!string.IsNullOrEmpty(plu.Itf14))
            Itf14 = plu.Itf14;
        IsCheckWeight = plu.IsCheckWeight;
        if (plu.Nomenclature.IsNotNew)
            Nomenclature = plu.Nomenclature.CloneCast();
    }

    #endregion
}