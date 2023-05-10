// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.PlusCharacteristics;

/// <summary>
/// Table "NOMENCLATURES_CHARACTERISTICS".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlPluCharacteristicModel : WsSqlTable1CBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual decimal AttachmentsCount { get; set; }
    /// <summary>
    /// GUID номенклатуры.
    /// </summary>
    [XmlIgnore] public virtual Guid NomenclatureGuid{ get; set; }


    public WsSqlPluCharacteristicModel() : base(WsSqlFieldIdentity.Uid)
    {
        AttachmentsCount = 0;
        NomenclatureGuid = Guid.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlPluCharacteristicModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        AttachmentsCount = info.GetDecimal(nameof(AttachmentsCount));
        object nomenclatureGuid = info.GetValue(nameof(NomenclatureGuid), typeof(Guid));
        NomenclatureGuid = nomenclatureGuid is Guid nomenclatureGuid2 ? nomenclatureGuid2 : Guid.Empty;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(AttachmentsCount)}: {AttachmentsCount}. ";

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

    public override object Clone()
    {
        WsSqlPluCharacteristicModel item = new();
        item.CloneSetup(base.CloneCast());
        item.AttachmentsCount = AttachmentsCount;
        item.NomenclatureGuid = NomenclatureGuid;
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
        info.AddValue(nameof(AttachmentsCount), AttachmentsCount);
        info.AddValue(nameof(NomenclatureGuid), NomenclatureGuid);
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPluCharacteristicModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(AttachmentsCount, item.AttachmentsCount) &&
        Equals(NomenclatureGuid, item.NomenclatureGuid);
    
    public new virtual WsSqlPluCharacteristicModel CloneCast() => (WsSqlPluCharacteristicModel)Clone();

    public override void UpdateProperties(WsSqlTable1CBase item)
    {
        base.UpdateProperties(item);
        // Get properties from /api/send_nomenclatures/.
        if (item is not WsSqlPluCharacteristicModel pluCharacteristic) throw new ArgumentException();
        Uid1C = pluCharacteristic.Uid1C;

        if (!Equals(pluCharacteristic.NomenclatureGuid, Guid.Empty))
            NomenclatureGuid = pluCharacteristic.NomenclatureGuid;
        if (pluCharacteristic.AttachmentsCount > 0)
            AttachmentsCount = pluCharacteristic.AttachmentsCount;
    }

    #endregion
}