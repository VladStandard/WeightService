namespace WsStorageCore.Tables.TableScaleFkModels.PlusFks;

[DebuggerDisplay("{ToString()}")]
public class WsSqlPluFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructornomenclatureCharacteristicsFk

    private WsSqlPluModel _plu;
    public virtual WsSqlPluModel Plu { get => _plu; set => _plu = value; }
    
    private WsSqlPluModel _parent;
    public virtual WsSqlPluModel Parent { get => _parent; set => _parent = value; }
    
    private WsSqlPluModel? _category;
    public virtual WsSqlPluModel? Category { get => _category; set => _category = value; }
    
    public WsSqlPluFkModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        _plu = new();
        _parent = new();
        _category = null;
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
    
    public override void FillProperties()
    {
        base.FillProperties();
        Plu.FillProperties();
        Parent.FillProperties();
        Category?.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPluFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Plu.Equals(item.Plu) &&
        Parent.Equals(item.Parent) &&
        (Category is null && item.Category is null || Category is not null && item.Category is not null && Category.Equals(item.Category));

    #endregion
}