namespace Ws.StorageCore.Entities.SchemaScale.PlusStorageMethodsFks;

[DebuggerDisplay("{ToString()}")]
public class SqlPluStorageMethodFkEntity : SqlEntityBase
{
    #region Public and private fields, properties, constructor

    public virtual SqlPluEntity Plu { get; set; }
    public virtual SqlPluStorageMethodEntity Method { get; set; }
    public virtual SqlTemplateResourceEntity Resource { get; set; }
    
    public SqlPluStorageMethodFkEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Plu = new();
        Method = new();
        Resource = new();
    }

    public SqlPluStorageMethodFkEntity(SqlPluStorageMethodFkEntity item) : base(item)
    {
        Plu = new(item.Plu);
        Method = new(item.Method);
        Resource = new(item.Resource);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(Plu)}: {Plu}. " + 
        $"{nameof(Method)}: {Method}. " +
        $"{nameof(Resource)}: {Resource}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlPluStorageMethodFkEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() && 
        Plu.EqualsDefault() &&
        Method.EqualsDefault() &&
        Resource.EqualsDefault();

    public override void FillProperties()
    {
        base.FillProperties();
        Plu.FillProperties();
        Method.FillProperties();
        Resource.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(SqlPluStorageMethodFkEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Plu.Equals(item.Plu) &&
        Method.Equals(item.Method) &&
        Resource.Equals(item.Resource);

    #endregion
}