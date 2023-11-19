namespace Ws.StorageCore.Entities.SchemaScale.PlusFks;

[DebuggerDisplay("{ToString()}")]
public class SqlPluFkEntity : SqlEntityBase
{
    #region Public and private fields, properties, constructornomenclatureCharacteristicsFk

    private SqlPluEntity _plu;
    public virtual SqlPluEntity Plu { get => _plu; set => _plu = value; }
    
    private SqlPluEntity _parent;
    public virtual SqlPluEntity Parent { get => _parent; set => _parent = value; }
    
    private SqlPluEntity? _category;
    public virtual SqlPluEntity? Category { get => _category; set => _category = value; }
    
    public SqlPluFkEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        _plu = new();
        _parent = new();
        _category = null;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(Plu)}: {Plu}. " +
        $"{nameof(Parent)}: {Parent}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlPluFkEntity)obj);
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

    public virtual bool Equals(SqlPluFkEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Plu.Equals(item.Plu) &&
        Parent.Equals(item.Parent) &&
        (Category is null && item.Category is null || Category is not null && item.Category is not null && Category.Equals(item.Category));

    #endregion
}