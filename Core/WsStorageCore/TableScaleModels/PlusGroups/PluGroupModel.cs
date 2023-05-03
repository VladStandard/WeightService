// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.PlusGroups;

/// <summary>
/// Table "PLUS_GROUPS".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(PluGroupModel)} | {IsMarked} | {IsGroup} | {Code} | {Name} | {Uid1C} ")]
public class PluGroupModel : WsSqlTable1CBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual bool IsGroup { get; set; }
    [XmlElement] public virtual string Code { get; set; }
    [XmlIgnore] public virtual Guid ParentGuid { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluGroupModel() : base(WsSqlFieldIdentity.Uid)
    {
        IsGroup = false;
        Code = string.Empty;
        ParentGuid = Guid.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PluGroupModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        IsGroup = info.GetBoolean(nameof(IsGroup));
        Code = info.GetString(nameof(Code));
        object groupGuid = info.GetValue(nameof(ParentGuid), typeof(Guid));
        ParentGuid = groupGuid is Guid guid ? guid : Guid.Empty;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(IsGroup)}: {IsGroup}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Code)}: {Code}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluGroupModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public new virtual bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(IsGroup, false) &&
        Equals(Code, string.Empty) &&
        Equals(ParentGuid, Guid.Empty);

    public override object Clone()
    {
        PluGroupModel item = new();
        item.CloneSetup(base.CloneCast());
        item.IsGroup = IsGroup;
        item.Code = Code;
        item.ParentGuid = ParentGuid;
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
        info.AddValue(nameof(Code), Code);
        info.AddValue(nameof(ParentGuid), ParentGuid);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Code = LocaleCore.Sql.SqlItemFieldCode;
    }

    public override void UpdateProperties(WsSqlTable1CBase item)
    {
        base.UpdateProperties(item);
        // Get properties from /api/send_nomenclatures_groups/.
        if (item is not PluGroupModel pluGroup) throw new ArgumentException();
        Uid1C = pluGroup.Uid1C;

        IsGroup = pluGroup.IsGroup;
        Code = pluGroup.Code;
        ParentGuid = pluGroup.ParentGuid;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(PluGroupModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(IsGroup, item.IsGroup) &&
        Equals(Code, item.Code) &&
        Equals(ParentGuid, item.ParentGuid);

    public new virtual PluGroupModel CloneCast() => (PluGroupModel)Clone();

    #endregion
}