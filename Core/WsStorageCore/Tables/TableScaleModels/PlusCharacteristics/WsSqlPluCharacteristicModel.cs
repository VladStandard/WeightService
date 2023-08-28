namespace WsStorageCore.Tables.TableScaleModels.PlusCharacteristics;

/// <summary>
/// Table "NOMENCLATURES_CHARACTERISTICS".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlPluCharacteristicModel : WsSqlTable1CBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual decimal AttachmentsCount { get; set; }
    [XmlIgnore] public virtual Guid NomenclatureGuid { get; set; }

    public WsSqlPluCharacteristicModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        AttachmentsCount = 0;
        NomenclatureGuid = Guid.Empty;
    }
    
    protected WsSqlPluCharacteristicModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        AttachmentsCount = info.GetDecimal(nameof(AttachmentsCount));
        object nomenclatureGuid = info.GetValue(nameof(NomenclatureGuid), typeof(Guid));
        NomenclatureGuid = nomenclatureGuid is Guid nomenclatureGuid2 ? nomenclatureGuid2 : Guid.Empty;
    }

    public WsSqlPluCharacteristicModel(WsSqlPluCharacteristicModel item) : base(item)
    {
        AttachmentsCount = item.AttachmentsCount;
        NomenclatureGuid = item.NomenclatureGuid;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | {Uid1C} | {Name} | {AttachmentsCount}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPluCharacteristicModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public new virtual bool EqualsDefault() =>
        base.EqualsDefault() && Equals(AttachmentsCount, (decimal)0) &&
        Equals(NomenclatureGuid, Guid.Empty);

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(AttachmentsCount), AttachmentsCount);
        info.AddValue(nameof(NomenclatureGuid), NomenclatureGuid);
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPluCharacteristicModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(AttachmentsCount, item.AttachmentsCount) &&
        Equals(NomenclatureGuid, item.NomenclatureGuid);
    
    public virtual void UpdateProperties(WsSqlPluCharacteristicModel item)
    {
        // Get properties from /api/send_nomenclatures/.
        Uid1C = item.Uid1C;
        if (!Equals(item.NomenclatureGuid, Guid.Empty))
            NomenclatureGuid = item.NomenclatureGuid;
        if (item.AttachmentsCount > 0)
            AttachmentsCount = item.AttachmentsCount;
    }

    #endregion
}