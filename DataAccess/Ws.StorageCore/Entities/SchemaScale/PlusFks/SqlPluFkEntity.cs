namespace Ws.StorageCore.Entities.SchemaScale.PlusFks;

[DebuggerDisplay("{ToString()}")]
public class SqlPluFkEntity : SqlEntityBase
{
    #region Public and private fields, properties, constructornomenclatureCharacteristicsFk
    
    public virtual SqlPluEntity Plu { get; set; }
    public virtual SqlPluEntity Parent { get; set; }
    public virtual SqlPluEntity? Category { get; set; }
    
    public SqlPluFkEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Plu = new();
        Parent = new();
        Category = null;
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
    
    public override void FillProperties()
    {
        base.FillProperties();
        Plu.FillProperties();
        Parent.FillProperties();
        Category?.FillProperties();
    }

    #endregion
    
    public virtual bool Equals(SqlPluFkEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Plu.Equals(item.Plu) &&
        Parent.Equals(item.Parent) &&
        (Category is null && item.Category is null || Category is not null && item.Category is not null && Category.Equals(item.Category));
}