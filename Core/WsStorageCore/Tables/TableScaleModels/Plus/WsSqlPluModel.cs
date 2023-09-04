// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

namespace WsStorageCore.Tables.TableScaleModels.Plus;

/// <summary>
/// Table "PLUS".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlPluModel : WsSqlTable1CBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual bool IsGroup { get; set; }
    [XmlElement] public virtual short Number { get; set; }
    [XmlElement] public virtual string Code { get; set; }
    [XmlElement] public virtual string FullName { get; set; }
    [XmlElement] public virtual byte ShelfLifeDays { get; set; }
    [XmlElement] public virtual string Gtin { get; set; }
    [XmlElement] public virtual string Ean13 { get; set; }
    [XmlElement] public virtual string Itf14 { get; set; }
    [XmlElement] public virtual bool IsCheckWeight { get; set; }
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
    /// Кол-во вложений.
    /// </summary>
    [XmlElement] public virtual short AttachmentsCount { get; set; }
    [XmlIgnore] public override string DisplayName => $"{Number} | {Name}";
    
    public WsSqlPluModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        BoxTypeGuid = Guid.Empty;
        BoxTypeName = string.Empty;
        BoxTypeWeight = default;
        BrandGuid = Guid.Empty;
        CategoryGuid = Guid.Empty;
        ClipTypeGuid = Guid.Empty;
        ClipTypeName = string.Empty;
        ClipTypeWeight = default;
        Code = string.Empty;
        Ean13 = string.Empty;
        FullName = string.Empty;
        GroupGuid = Guid.Empty;
        Gtin = string.Empty;
        IsCheckWeight = false;
        IsGroup = default;
        Itf14 = string.Empty;
        MeasurementType = string.Empty;
        Number = default;
        PackageTypeGuid = Guid.Empty;
        PackageTypeName = string.Empty;
        PackageTypeWeight = default;
        ParentGuid = Guid.Empty;
        ShelfLifeDays = default;
        AttachmentsCount = default;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlPluModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        IsGroup = info.GetBoolean(nameof(IsGroup));
        Number = info.GetInt16(nameof(Number));
        FullName = info.GetString(nameof(FullName));
        ShelfLifeDays = info.GetByte(nameof(ShelfLifeDays));
        Gtin = info.GetString(nameof(Gtin));
        Ean13 = info.GetString(nameof(Ean13));
        Itf14 = info.GetString(nameof(Itf14));
        Code = info.GetString(nameof(Code));
        IsCheckWeight = info.GetBoolean(nameof(IsCheckWeight));
        object parentGroupGuid = info.GetValue(nameof(ParentGuid), typeof(Guid));
        ParentGuid = parentGroupGuid is Guid parentGroupGuid2 ? parentGroupGuid2 : Guid.Empty;
        object groupGuid = info.GetValue(nameof(GroupGuid), typeof(Guid));
        GroupGuid = groupGuid is Guid groupGuid2 ? groupGuid2 : Guid.Empty;
        MeasurementType = info.GetString(nameof(MeasurementType));
        object boxTypeGuid = info.GetValue(nameof(BoxTypeGuid), typeof(Guid));
        BoxTypeGuid = boxTypeGuid is Guid boxTypeGuid2 ? boxTypeGuid2 : Guid.Empty;
        BoxTypeName = info.GetString(nameof(BoxTypeName));
        BoxTypeWeight = info.GetDecimal(nameof(BoxTypeWeight));
        object clipTypeGuid = info.GetValue(nameof(ClipTypeGuid), typeof(Guid));
        ClipTypeGuid = clipTypeGuid is Guid clipTypeGuid2 ? clipTypeGuid2 : Guid.Empty;
        ClipTypeName = info.GetString(nameof(ClipTypeName));
        ClipTypeWeight = info.GetDecimal(nameof(ClipTypeWeight));
        object packageTypeGuid = info.GetValue(nameof(PackageTypeGuid), typeof(Guid));
        PackageTypeGuid = packageTypeGuid is Guid packageTypeGuid2 ? packageTypeGuid2 : Guid.Empty;
        PackageTypeName = info.GetString(nameof(PackageTypeName));
        PackageTypeWeight = info.GetDecimal(nameof(PackageTypeWeight));
        AttachmentsCount = info.GetInt16(nameof(AttachmentsCount));
    }

    public WsSqlPluModel(WsSqlPluModel item) : base(item)
    {
        IsGroup = item.IsGroup;
        ParentGuid = item.ParentGuid;
        GroupGuid = item.GroupGuid;
        BoxTypeGuid = item.BoxTypeGuid;
        BoxTypeName = item.BoxTypeName;
        BoxTypeWeight = item.BoxTypeWeight;
        ClipTypeGuid = item.ClipTypeGuid;
        ClipTypeName = item.ClipTypeName;
        ClipTypeWeight = item.ClipTypeWeight;
        PackageTypeGuid = item.PackageTypeGuid;
        PackageTypeName = item.PackageTypeName;
        PackageTypeWeight = item.PackageTypeWeight;
        Code = item.Code;
        Number = item.Number;
        FullName = item.FullName;
        ShelfLifeDays = item.ShelfLifeDays;
        Gtin = item.Gtin;
        Ean13 = item.Ean13;
        Itf14 = item.Itf14;
        IsCheckWeight = item.IsCheckWeight;
        AttachmentsCount = item.AttachmentsCount;
        MeasurementType = item.MeasurementType;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{Number} | {Name} | {Uid1C} | {GetIsMarked()} | {GetIsGroup()} | {Code}";

    public virtual string GetIsGroup() => IsGroup? "Is group" : "Is not group";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPluModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(IsGroup, default(bool)) &&
        Equals(ParentGuid, Guid.Empty) &&
        Equals(GroupGuid, Guid.Empty) &&
        Equals(BoxTypeGuid, Guid.Empty) &&
        Equals(BoxTypeName, string.Empty) &&
        Equals(BoxTypeWeight, default(decimal)) &&
        Equals(ClipTypeGuid, Guid.Empty) &&
        Equals(ClipTypeName, string.Empty) &&
        Equals(ClipTypeWeight, default(decimal)) &&
        Equals(PackageTypeGuid, Guid.Empty) &&
        Equals(PackageTypeName, string.Empty) &&
        Equals(PackageTypeWeight, default(decimal)) &&
        Equals(Code, string.Empty) &&
        Equals(Number, default(short)) &&
        Equals(FullName, string.Empty) &&
        Equals(ShelfLifeDays, default(byte)) &&
        Equals(Gtin, string.Empty) &&
        Equals(Ean13, string.Empty) &&
        Equals(Itf14, string.Empty) &&
        Equals(IsCheckWeight, false) &&
        Equals(AttachmentsCount, default(short));

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(IsGroup), IsGroup);
        info.AddValue(nameof(ParentGuid), ParentGuid);
        info.AddValue(nameof(GroupGuid), GroupGuid);
        info.AddValue(nameof(Number), Number);
        info.AddValue(nameof(FullName), FullName);
        info.AddValue(nameof(ShelfLifeDays), ShelfLifeDays);
        info.AddValue(nameof(Gtin), Gtin);
        info.AddValue(nameof(Ean13), Ean13);
        info.AddValue(nameof(Itf14), Itf14);
        info.AddValue(nameof(IsCheckWeight), IsCheckWeight);
        info.AddValue(nameof(MeasurementType), MeasurementType);
        info.AddValue(nameof(BoxTypeGuid), BoxTypeGuid);
        info.AddValue(nameof(BoxTypeName), BoxTypeName);
        info.AddValue(nameof(BoxTypeWeight), BoxTypeWeight);
        info.AddValue(nameof(ClipTypeGuid), ClipTypeGuid);
        info.AddValue(nameof(ClipTypeName), ClipTypeName);
        info.AddValue(nameof(ClipTypeWeight), ClipTypeWeight);
        info.AddValue(nameof(PackageTypeGuid), PackageTypeGuid);
        info.AddValue(nameof(PackageTypeName), PackageTypeName);
        info.AddValue(nameof(PackageTypeWeight), PackageTypeWeight);
        info.AddValue(nameof(AttachmentsCount), AttachmentsCount);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Code = WsLocaleCore.Sql.SqlItemFieldCode;
        Number = 100;
        FullName = WsLocaleCore.Sql.SqlItemFieldFullName;
        Gtin = WsLocaleCore.Sql.SqlItemFieldGtin;
        Ean13 = WsLocaleCore.Sql.SqlItemFieldEan13;
        Itf14 = WsLocaleCore.Sql.SqlItemFieldItf14;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPluModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(IsGroup, item.IsGroup) &&
        Equals(ParentGuid, item.ParentGuid) &&
        Equals(GroupGuid, item.GroupGuid) &&
        Equals(BoxTypeGuid, item.BoxTypeGuid) &&
        Equals(BoxTypeName, item.BoxTypeName) &&
        Equals(BoxTypeWeight, item.BoxTypeWeight) &&
        Equals(ClipTypeGuid, item.ClipTypeGuid) &&
        Equals(ClipTypeName, item.ClipTypeName) &&
        Equals(ClipTypeWeight, item.ClipTypeWeight) &&
        Equals(PackageTypeGuid, item.PackageTypeGuid) &&
        Equals(PackageTypeName, item.PackageTypeName) &&
        Equals(PackageTypeWeight, item.PackageTypeWeight) &&
        Equals(Code, item.Code) &&
        Equals(Number, item.Number) &&
        Equals(FullName, item.FullName) &&
        Equals(ShelfLifeDays, item.ShelfLifeDays) &&
        Equals(Gtin, item.Gtin) &&
        Equals(Ean13, item.Ean13) &&
        Equals(Itf14, item.Itf14) &&
        Equals(IsCheckWeight, item.IsCheckWeight) &&
        Equals(AttachmentsCount, item.AttachmentsCount);

    public virtual void UpdateProperties(WsSqlPluModel plu)
    {
        // Get properties from /api/send_nomenclatures/.
        Uid1C = plu.Uid1C;
        IsGroup = plu.IsGroup;
        if (!IsGroup && Equals(plu.Number, (short)0)) throw new ArgumentException(nameof(Number));
        Number = plu.Number;
        if (string.IsNullOrEmpty(plu.Code)) throw new ArgumentException(nameof(Code));
        Code = plu.Code;
        if (!IsGroup && string.IsNullOrEmpty(plu.FullName)) throw new ArgumentException(nameof(FullName));
        FullName = plu.FullName;
        if (!IsGroup && plu.ShelfLifeDays <= 0) throw new ArgumentException(nameof(ShelfLifeDays));
        ShelfLifeDays = plu.ShelfLifeDays;
        IsCheckWeight = plu.IsCheckWeight;
        
        if (!string.IsNullOrEmpty(plu.Gtin))
            Gtin = plu.Gtin;
        if (!string.IsNullOrEmpty(plu.Ean13))
            Ean13 = plu.Ean13;
        
        Itf14 = plu.Itf14;
        
        if (!Equals(plu.ParentGuid, Guid.Empty))
            ParentGuid = plu.ParentGuid;
        if (!Equals(plu.GroupGuid, Guid.Empty))
            GroupGuid = plu.GroupGuid;
        if (!Equals(plu.BoxTypeGuid, Guid.Empty))
            BoxTypeGuid = plu.BoxTypeGuid;
        if (!Equals(plu.ClipTypeGuid, Guid.Empty))
            ClipTypeGuid = plu.ClipTypeGuid;
        if (!Equals(plu.PackageTypeGuid, Guid.Empty))
            PackageTypeGuid = plu.PackageTypeGuid;
        if (!string.IsNullOrEmpty(plu.BoxTypeName))
            BoxTypeName = plu.BoxTypeName;
        if (plu.BoxTypeWeight > 0)
            BoxTypeWeight = plu.BoxTypeWeight;
        if (!string.IsNullOrEmpty(plu.ClipTypeName))
            ClipTypeName = plu.ClipTypeName;
        if (plu.ClipTypeWeight > 0)
            ClipTypeWeight = plu.ClipTypeWeight;
        if (!string.IsNullOrEmpty(plu.PackageTypeName))
            PackageTypeName = plu.PackageTypeName;
        if (plu.PackageTypeWeight > 0)
            PackageTypeWeight = plu.PackageTypeWeight;
        
        if (!IsGroup && plu.AttachmentsCount <= 0) throw new ArgumentException(nameof(AttachmentsCount));
        AttachmentsCount = plu.AttachmentsCount;
    }

    public virtual void UpdateGtin()
    {
        Gtin = IsCheckWeight ? "0" + Ean13 : Itf14;
    }

    #endregion
}