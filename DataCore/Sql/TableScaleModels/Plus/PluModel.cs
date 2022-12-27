// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels.Nomenclatures;
using DataCore.Sql.Xml;

namespace DataCore.Sql.TableScaleModels.Plus;

/// <summary>
/// Table "PLUS".
/// </summary>
[Serializable]
[DebuggerDisplay("Type = {nameof(PluModel)} | Number = {Number} | Name = {Name} | Code = {Nomenclature.Code}")]
public class PluModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual int Number { get; set; }
    
    [XmlElement]
    public virtual string NumberFormat
    {
        get => $"{Number:000}";
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement] public virtual string FullName { get; set; }
    [XmlElement] public virtual short ShelfLifeDays { get; set; }
    [XmlElement] public virtual int BoxQuantly { get; set; }
    [XmlElement] public virtual string Gtin { get; set; }
    [XmlElement]
    public virtual string Gtin14Format
    {
        get
        {
            return Gtin.Length switch
            {
                13 => BarcodeHelper.Instance.GetGtinWithCheckDigit(Gtin[..13]),
                14 => Gtin,
                _ => "ERROR"
            };
        }
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement] public virtual string Ean13 { get; set; }
    [XmlElement] public virtual string Itf14 { get; set; }
    [XmlElement] public virtual decimal UpperThreshold { get; set; }
    [XmlElement] public virtual decimal NominalWeight { get; set; }
    [XmlElement] public virtual decimal LowerThreshold { get; set; }
    [XmlElement] public virtual bool IsCheckWeight { get; set; }
    [XmlElement] public virtual NomenclatureModel Nomenclature { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluModel() : base(SqlFieldIdentityEnum.Uid)
    {
        Number = 0;
        FullName = string.Empty;
        ShelfLifeDays = 0;
        BoxQuantly = 0;
        Gtin = string.Empty;
        Ean13 = string.Empty;
        Itf14 = string.Empty;
        UpperThreshold = 0;
        NominalWeight = 0;
        LowerThreshold = 0;
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
        Number = info.GetInt32(nameof(Number));
        FullName = info.GetString(nameof(FullName));
        ShelfLifeDays = info.GetInt16(nameof(ShelfLifeDays));
        BoxQuantly = info.GetInt32(nameof(BoxQuantly));
        Gtin = info.GetString(nameof(Gtin));
        Ean13 = info.GetString(nameof(Ean13));
        Itf14 = info.GetString(nameof(Itf14));
        UpperThreshold = info.GetDecimal(nameof(UpperThreshold));
        NominalWeight = info.GetDecimal(nameof(NominalWeight));
        LowerThreshold = info.GetDecimal(nameof(LowerThreshold));
        IsCheckWeight = info.GetBoolean(nameof(IsCheckWeight));
        Nomenclature = (NomenclatureModel)info.GetValue(nameof(Nomenclature), typeof(NomenclatureModel));
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
        Equals(BoxQuantly, default(int)) &&
        Equals(Gtin, string.Empty) &&
        Equals(Ean13, string.Empty) &&
        Equals(Itf14, string.Empty) &&
        Equals(UpperThreshold, default(decimal)) &&
        Equals(NominalWeight, default(decimal)) &&
        Equals(LowerThreshold, default(decimal)) &&
        Equals(IsCheckWeight, false) &&
        Nomenclature.EqualsDefault();

    public override object Clone()
    {
        PluModel item = new();
        item.Number = Number;
        item.FullName = FullName;
        item.ShelfLifeDays = ShelfLifeDays;
        item.BoxQuantly = BoxQuantly;
        item.Gtin = Gtin;
        item.Ean13 = Ean13;
        item.Itf14 = Itf14;
        item.UpperThreshold = UpperThreshold;
        item.NominalWeight = NominalWeight;
        item.LowerThreshold = LowerThreshold;
        item.IsCheckWeight = IsCheckWeight;
        item.Nomenclature = Nomenclature.CloneCast();
        item.CloneSetup(base.CloneCast());
        return item;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Number), Number);
        info.AddValue(nameof(FullName), FullName);
        info.AddValue(nameof(ShelfLifeDays), ShelfLifeDays);
        info.AddValue(nameof(BoxQuantly), BoxQuantly);
        info.AddValue(nameof(Gtin), Gtin);
        info.AddValue(nameof(Ean13), Ean13);
        info.AddValue(nameof(Itf14), Itf14);
        info.AddValue(nameof(UpperThreshold), UpperThreshold);
        info.AddValue(nameof(NominalWeight), NominalWeight);
        info.AddValue(nameof(LowerThreshold), LowerThreshold);
        info.AddValue(nameof(IsCheckWeight), IsCheckWeight);
        info.AddValue(nameof(Nomenclature), Nomenclature);
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
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(PluModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Number, item.Number) &&
        Equals(FullName, item.FullName) &&
        Equals(ShelfLifeDays, item.ShelfLifeDays) &&
        Equals(BoxQuantly, item.BoxQuantly) &&
        Equals(Gtin, item.Gtin) &&
        Equals(Ean13, item.Ean13) &&
        Equals(Itf14, item.Itf14) &&
        Equals(UpperThreshold, item.UpperThreshold) &&
        Equals(NominalWeight, item.NominalWeight) &&
        Equals(LowerThreshold, item.LowerThreshold) &&
        Equals(IsCheckWeight, item.IsCheckWeight) &&
        Nomenclature.Equals(item.Nomenclature);

    public new virtual PluModel CloneCast() => (PluModel)Clone();

    #endregion
}
