// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Enums;
using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels.PlusGroups;

namespace DataCore.Sql.TableScaleFkModels.PlusGroupsFks;

/// <summary>
/// Table "PLUS_GROUPS_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(PluGroupFkModel)} | {ToString()}")]
public class PluGroupFkModel : SqlTableBase
{
    #region Public and private fields, properties, constructornomenclatureCharacteristicsFk

    private PluGroupModel _pluGroup;
    [XmlElement] public virtual PluGroupModel PluGroup { get => _pluGroup; set => _pluGroup = value; }
    private PluGroupModel _parent;
    [XmlElement] public virtual PluGroupModel Parent { get => _parent; set => _parent = value; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluGroupFkModel() : base(SqlFieldIdentity.Uid)
    {
        _pluGroup = new();
        _parent = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PluGroupFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        _pluGroup = (PluGroupModel)info.GetValue(nameof(_pluGroup), typeof(PluGroupModel));
        _parent = (PluGroupModel)info.GetValue(nameof(_parent), typeof(PluGroupModel));
    }

    #endregion

    #region Public and private methods - override

    /// <summary>
    /// To string.
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(PluGroup)}: {PluGroup}. " +
        $"{nameof(Parent)}: {Parent}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluGroupFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        PluGroup.EqualsDefault() &&
        Parent.EqualsDefault();

    public override object Clone()
    {
        PluGroupFkModel item = new();
        item.PluGroup = PluGroup.CloneCast();
        item.Parent = Parent.CloneCast();
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
        info.AddValue(nameof(PluGroup), PluGroup);
        info.AddValue(nameof(Parent), Parent);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        PluGroup.FillProperties();
        Parent.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(PluGroupFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        PluGroup.Equals(item.PluGroup) &&
        Parent.Equals(item.Parent);

    public new virtual PluGroupFkModel CloneCast() => (PluGroupFkModel)Clone();

    #endregion
}