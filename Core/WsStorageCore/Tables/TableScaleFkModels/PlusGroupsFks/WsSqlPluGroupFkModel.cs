// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleFkModels.PlusGroupsFks;

[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlPluGroupFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructornomenclatureCharacteristicsFk

    private WsSqlPluGroupModel _pluGroup;
    [XmlElement] public virtual WsSqlPluGroupModel PluGroup { get => _pluGroup; set => _pluGroup = value; }
    private WsSqlPluGroupModel _parent;
    [XmlElement] public virtual WsSqlPluGroupModel Parent { get => _parent; set => _parent = value; }
    
    public WsSqlPluGroupFkModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        _pluGroup = new();
        _parent = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlPluGroupFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        _pluGroup = (WsSqlPluGroupModel)info.GetValue(nameof(_pluGroup), typeof(WsSqlPluGroupModel));
        _parent = (WsSqlPluGroupModel)info.GetValue(nameof(_parent), typeof(WsSqlPluGroupModel));
    }

    public WsSqlPluGroupFkModel(WsSqlPluGroupFkModel item) : base(item)
    {
        PluGroup = new(item.PluGroup);
        Parent = new(item.Parent);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(PluGroup)}: {PluGroup}. " +
        $"{nameof(Parent)}: {Parent}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPluGroupFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        PluGroup.EqualsDefault() &&
        Parent.EqualsDefault();

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

    public virtual void UpdateProperties(WsSqlPluGroupFkModel item)
    {
        // Get properties from /api/send_nomenclatures/.
        base.UpdateProperties(item, true);
        
        PluGroup = new(item.PluGroup);
        Parent = new(item.Parent);
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPluGroupFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        PluGroup.Equals(item.PluGroup) &&
        Parent.Equals(item.Parent);

    #endregion
}