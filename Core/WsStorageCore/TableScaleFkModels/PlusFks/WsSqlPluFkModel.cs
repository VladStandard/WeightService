// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusFks;

/// <summary>
/// Table "PLUS_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlPluFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructornomenclatureCharacteristicsFk

    private WsSqlPluModel _plu;
    [XmlElement] public virtual WsSqlPluModel Plu { get => _plu; set => _plu = value; }
    private WsSqlPluModel _parent;
    [XmlElement] public virtual WsSqlPluModel Parent { get => _parent; set => _parent = value; }
    private WsSqlPluModel? _category;
    [XmlElement] public virtual WsSqlPluModel? Category { get => _category; set => _category = value; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPluFkModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        _plu = new();
        _parent = new();
        _category = null;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlPluFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        _plu = (WsSqlPluModel)info.GetValue(nameof(_plu), typeof(WsSqlPluModel));
        _parent = (WsSqlPluModel)info.GetValue(nameof(_parent), typeof(WsSqlPluModel));
        _category = (WsSqlPluModel)info.GetValue(nameof(_category), typeof(WsSqlPluModel));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Plu)}: {Plu}. " +
        $"{nameof(Parent)}: {Parent}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPluFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Plu.EqualsDefault() &&
        Parent.EqualsDefault() &&
        Category is null;

    public override object Clone()
    {
        WsSqlPluFkModel item = new();
        item.CloneSetup(base.CloneCast());
        item.Plu = Plu.CloneCast();
        item.Parent = Parent.CloneCast();
        item.Category = Category?.CloneCast();
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
        info.AddValue(nameof(Plu), Plu);
        info.AddValue(nameof(Parent), Parent);
        info.AddValue(nameof(Category), Category);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Plu.FillProperties();
        Parent.FillProperties();
        Category?.FillProperties();
    }

    public virtual void UpdateProperties(WsSqlPluFkModel pluFk)
    {
        // Get properties from /api/send_nomenclatures/.
        base.UpdateProperties(pluFk, true);
        
        Plu = pluFk.Plu;
        Parent = pluFk.Parent;
        Category = pluFk.Category;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPluFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Plu.Equals(item.Plu) &&
        Parent.Equals(item.Parent) &&
        (Category is null && item.Category is null || Category is not null && item.Category is not null && Category.Equals(item.Category));

    public new virtual WsSqlPluFkModel CloneCast() => (WsSqlPluFkModel)Clone();

    #endregion
}