// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels.Nomenclatures;

/// <summary>
/// Table "NOMENCLATURES".
/// </summary>
[Serializable]
public class NomenclatureV2Model : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual bool IsGroup { get; set; }
    [XmlElement] public virtual string FullName { get; set; }
    [XmlElement] public virtual string Code { get; set; }
    [XmlElement] public virtual string MeasurementType { get; set; }
    [XmlIgnore] public virtual bool IsWeighted => MeasurementType.ToUpper().Equals("КГ");
    [XmlElement] public virtual short AttachmentsCount { get; set; }
    [XmlElement] public virtual short ShelfLife { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public NomenclatureV2Model() : base(SqlFieldIdentityEnum.Uid)
    {
        IsGroup = false;
        FullName = string.Empty;
        Code = string.Empty;
        MeasurementType = string.Empty;
        AttachmentsCount = 0;
        ShelfLife = 0;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private NomenclatureV2Model(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        IsGroup = info.GetBoolean(nameof(IsGroup));
        FullName = info.GetString(nameof(FullName));
        Code = info.GetString(nameof(Code));
        MeasurementType = info.GetString(nameof(MeasurementType));
        AttachmentsCount = info.GetInt16(nameof(AttachmentsCount));
        ShelfLife = info.GetInt16(nameof(ShelfLife));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(IsGroup)}: {IsGroup}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Code)}: {Code}. " +
        $"{nameof(MeasurementType)}: {MeasurementType}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((NomenclatureV2Model)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public new virtual bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(IsGroup, false) &&
        Equals(FullName, string.Empty) &&
        Equals(Code, string.Empty) &&
        Equals(MeasurementType, string.Empty) &&
        Equals(AttachmentsCount, (short)0) &&
        Equals(ShelfLife, (short)0);

    public override object Clone()
    {
        NomenclatureV2Model item = new();
        item.IsGroup = IsGroup;
        item.FullName = FullName;
        item.Code = Code;
        item.AttachmentsCount = AttachmentsCount;
        item.ShelfLife = ShelfLife;
        item.CloneSetup(base.CloneCast());
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
        info.AddValue(nameof(IsGroup), IsGroup);
        info.AddValue(nameof(FullName), FullName);
        info.AddValue(nameof(Code), Code);
        info.AddValue(nameof(AttachmentsCount), AttachmentsCount);
        info.AddValue(nameof(ShelfLife), ShelfLife);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        FullName = LocaleCore.Sql.SqlItemFieldFullName;
        Code = LocaleCore.Sql.SqlItemFieldCode;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(NomenclatureV2Model item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(IsGroup, item.IsGroup) &&
        Equals(FullName, item.FullName) &&
        Equals(Code, item.Code) &&
        Equals(AttachmentsCount, item.AttachmentsCount) &&
        Equals(ShelfLife, item.ShelfLife);

    public new virtual NomenclatureV2Model CloneCast() => (NomenclatureV2Model)Clone();

    #endregion
}
