// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Enums;
using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels.PlusGroups;

/// <summary>
/// Table "PLUS_GROUPS".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(PluGroupModel)} | {nameof(Uid1C)} = {Uid1C} | {nameof(IsGroup)} = {IsGroup} | {Code}")]
public class PluGroupModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual bool IsGroup { get; set; }
    [XmlElement] public virtual string Code { get; set; }
    [XmlIgnore] public virtual Guid ParentGuid { get; set; }
    [XmlIgnore] public virtual Guid Uid1C { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluGroupModel() : base(SqlFieldIdentity.Uid)
    {
        IsGroup = false;
        Code = string.Empty;
        ParentGuid = Guid.Empty;
        Uid1C = Guid.Empty;
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
        Uid1C = info.GetValue(nameof(Uid1C), typeof(Guid)) is Guid uid1C ? uid1C : Guid.Empty;
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
        Equals(ParentGuid, Guid.Empty) &&
        Equals(Uid1C, Guid.Empty);

    public override object Clone()
    {
        PluGroupModel item = new();
        item.CloneSetup(base.CloneCast());
        item.IsGroup = IsGroup;
        item.Code = Code;
        item.ParentGuid = ParentGuid;
        item.Uid1C = Uid1C;
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
        info.AddValue(nameof(Uid1C), Uid1C);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Code = LocaleCore.Sql.SqlItemFieldCode;
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