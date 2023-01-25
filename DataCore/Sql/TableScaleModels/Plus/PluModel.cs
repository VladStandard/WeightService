// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

using DataCore.Sql.Core.Enums;
using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels.Nomenclatures;
using DataCore.Sql.Xml;

namespace DataCore.Sql.TableScaleModels.Plus;

/// <summary>
/// Table "PLUS".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(PluModel)} | {Name} | {nameof(Number)} = {Number}")]
public class PluModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual int Number { get; set; }
    [XmlElement] public virtual string NumberFormat
    {
        get => $"{Number:000}";
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement] public virtual string FullName { get; set; }
    [XmlElement] public virtual short ShelfLifeDays { get; set; }
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
    [XmlElement] public virtual NomenclatureV2Model Nomenclatures { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluModel() : base(SqlFieldIdentityEnum.Uid)
    {
        Number = 0;
        FullName = string.Empty;
        ShelfLifeDays = 0;
        Gtin = string.Empty;
        Ean13 = string.Empty;
        Itf14 = string.Empty;
        IsCheckWeight = false;
        Nomenclature = new();
        Nomenclatures = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PluModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Number = info.GetInt32(nameof(Number));
        FullName = info.GetString(nameof(FullName));
        ShelfLifeDays = info.GetInt16(nameof(ShelfLifeDays));
        Gtin = info.GetString(nameof(Gtin));
        Ean13 = info.GetString(nameof(Ean13));
        Itf14 = info.GetString(nameof(Itf14));
        IsCheckWeight = info.GetBoolean(nameof(IsCheckWeight));
        Nomenclature = (NomenclatureModel)info.GetValue(nameof(Nomenclature), typeof(NomenclatureModel));
        Nomenclatures = (NomenclatureV2Model)info.GetValue(nameof(Nomenclatures), typeof(NomenclatureV2Model));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. " +
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
        Equals(Number, default(int)) &&
        Equals(FullName, string.Empty) &&
        Equals(ShelfLifeDays, default(short)) &&
        Equals(Gtin, string.Empty) &&
        Equals(Ean13, string.Empty) &&
        Equals(Itf14, string.Empty) &&
        Equals(IsCheckWeight, false) &&
        Nomenclature.EqualsDefault() &&
        Nomenclatures.EqualsDefault();

    public override object Clone()
    {
        PluModel item = new();
        item.Number = Number;
        item.FullName = FullName;
        item.ShelfLifeDays = ShelfLifeDays;
        item.Gtin = Gtin;
        item.Ean13 = Ean13;
        item.Itf14 = Itf14;
        item.IsCheckWeight = IsCheckWeight;
        item.Nomenclature = Nomenclature.CloneCast();
        item.Nomenclatures = Nomenclatures.CloneCast();
        item.CloneSetup(base.CloneCast());
        return item;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Number), Number);
        info.AddValue(nameof(FullName), FullName);
        info.AddValue(nameof(ShelfLifeDays), ShelfLifeDays);
        info.AddValue(nameof(Gtin), Gtin);
        info.AddValue(nameof(Ean13), Ean13);
        info.AddValue(nameof(Itf14), Itf14);
        info.AddValue(nameof(IsCheckWeight), IsCheckWeight);
        info.AddValue(nameof(Nomenclature), Nomenclature);
        info.AddValue(nameof(Nomenclatures), Nomenclatures);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Number = 100;
        FullName = LocaleCore.Sql.SqlItemFieldFullName;
        Gtin = LocaleCore.Sql.SqlItemFieldGtin;
        Ean13 = LocaleCore.Sql.SqlItemFieldEan13;
        Itf14 = LocaleCore.Sql.SqlItemFieldItf14;
        Nomenclature.FillProperties();
        Nomenclatures.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(PluModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Number, item.Number) &&
        Equals(FullName, item.FullName) &&
        Equals(ShelfLifeDays, item.ShelfLifeDays) &&
        Equals(Gtin, item.Gtin) &&
        Equals(Ean13, item.Ean13) &&
        Equals(Itf14, item.Itf14) &&
        Equals(IsCheckWeight, item.IsCheckWeight) &&
        Nomenclature.Equals(item.Nomenclature) &&
        Nomenclatures.Equals(item.Nomenclatures);

    public new virtual PluModel CloneCast() => (PluModel)Clone();

    #endregion
}
