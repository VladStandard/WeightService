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
    /// Родитель.
    /// </summary>
    [XmlIgnore] public virtual Guid ParentGuid { get; set; }
    /// <summary>
    /// Группа 1 уровня.
    /// </summary>
    [XmlIgnore] public virtual Guid CategoryGuid { get; set; }
    /// <summary>
    /// Бренд.
    /// </summary>
    [XmlIgnore] public virtual Guid BrandGuid { get; set; }
    /// <summary>
    /// БазоваяЕдиницаИзмерения.
    /// </summary>
    [XmlIgnore] public virtual string MeasurementType { get; set; }
    /// <summary>
    /// НоменклатурнаяГруппа.
    /// </summary>
    [XmlIgnore] public virtual Guid GroupGuid { get; set; }
    /// <summary>
    /// ВидКоробки.
    /// </summary>
    [XmlIgnore] public virtual Guid BoxTypeGuid { get; set; }
    /// <summary>
    /// ИмяКоробки.
    /// </summary>
    [XmlIgnore] public virtual string BoxTypeName { get; set; }
    /// <summary>
    /// ВесКоробки.
    /// </summary>
    [XmlIgnore] public virtual decimal BoxTypeWeight { get; set; }
    /// <summary>
    /// ВидПакета.
    /// </summary>
    [XmlIgnore] public virtual Guid PackageTypeGuid { get; set; }
    /// <summary>
    /// ИмяПакета.
    /// </summary>
    [XmlIgnore] public virtual string PackageTypeName { get; set; }
    /// <summary>
    /// ВесПакета.
    /// </summary>
    [XmlIgnore] public virtual decimal PackageTypeWeight { get; set; }
    /// <summary>
    /// ВС_ВидКлипсы.
    /// </summary>
    [XmlIgnore] public virtual Guid ClipTypeGuid { get; set; }
    /// <summary>
    /// ИмяКлипсы.
    /// </summary>
    [XmlIgnore] public virtual string ClipTypeName { get; set; }
    /// <summary>
    /// ВесКлипсы.
    /// </summary>
    [XmlIgnore] public virtual decimal ClipTypeWeight { get; set; }

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
        MeasurementType = string.Empty;
        BoxTypeName = string.Empty;
        PackageTypeName = string.Empty;
        ClipTypeName = string.Empty;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PluModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        IsGroup = info.GetBoolean(nameof(IsGroup));
        Number = info.GetUInt16(nameof(Number));
        FullName = info.GetString(nameof(FullName));
        ShelfLifeDays = info.GetUInt16(nameof(ShelfLifeDays));
        Gtin = info.GetString(nameof(Gtin));
        Ean13 = info.GetString(nameof(Ean13));
        Itf14 = info.GetString(nameof(Itf14));
        Code = info.GetString(nameof(Code));
        IsCheckWeight = info.GetBoolean(nameof(IsCheckWeight));
        Nomenclature = (NomenclatureModel)info.GetValue(nameof(Nomenclature), typeof(NomenclatureModel));
        object parentGroupGuid = info.GetValue(nameof(ParentGuid), typeof(Guid));
        ParentGuid = parentGroupGuid is Guid parentGroupGuid2 ? parentGroupGuid2 : Guid.Empty;
        MeasurementType = info.GetString(nameof(MeasurementType));
        object boxTypeGuid = info.GetValue(nameof(BoxTypeGuid), typeof(Guid));
        BoxTypeGuid = boxTypeGuid is Guid boxTypeGuid2 ? boxTypeGuid2 : Guid.Empty;
        BoxTypeName = info.GetString(nameof(BoxTypeName));
        BoxTypeWeight = info.GetDecimal(nameof(BoxTypeWeight));
        object packageTypeGuid = info.GetValue(nameof(PackageTypeGuid), typeof(Guid));
        PackageTypeGuid = packageTypeGuid is Guid packageTypeGuid2 ? packageTypeGuid2 : Guid.Empty;
        PackageTypeName = info.GetString(nameof(PackageTypeName));
        PackageTypeWeight = info.GetDecimal(nameof(PackageTypeWeight));
        object clipTypeGuid = info.GetValue(nameof(ClipTypeGuid), typeof(Guid));
        ClipTypeGuid = clipTypeGuid is Guid clipTypeGuid2 ? clipTypeGuid2 : Guid.Empty;
        ClipTypeName = info.GetString(nameof(ClipTypeName));
        ClipTypeWeight = info.GetDecimal(nameof(ClipTypeWeight));
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
        info.AddValue(nameof(MeasurementType), MeasurementType);
        info.AddValue(nameof(ParentGuid), ParentGuid);
        info.AddValue(nameof(BoxTypeGuid), BoxTypeGuid);
        info.AddValue(nameof(BoxTypeName), BoxTypeName);
        info.AddValue(nameof(BoxTypeWeight), BoxTypeWeight);
        info.AddValue(nameof(PackageTypeGuid), PackageTypeGuid);
        info.AddValue(nameof(PackageTypeName), PackageTypeName);
        info.AddValue(nameof(PackageTypeWeight), PackageTypeWeight);
        info.AddValue(nameof(ClipTypeGuid), ClipTypeGuid);
        info.AddValue(nameof(ClipTypeName), ClipTypeName);
        info.AddValue(nameof(ClipTypeWeight), ClipTypeWeight);
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