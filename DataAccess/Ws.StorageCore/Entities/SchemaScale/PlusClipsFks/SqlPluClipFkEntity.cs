namespace Ws.StorageCore.Entities.SchemaScale.PlusClipsFks;

[DebuggerDisplay("{ToString()}")]
public class SqlPluClipFkEntity : SqlEntityBase
{
    #region Public and private fields, properties, constructor

    public virtual SqlClipEntity Clip { get; set; }
    public virtual SqlPluEntity Plu { get; set; }
    
    public SqlPluClipFkEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Clip = new();
        Plu = new();

    }
    
    public SqlPluClipFkEntity(SqlPluClipFkEntity item) : base(item)
    {
        Clip = new(item.Clip);
        Plu = new(item.Plu);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(Plu)}: {Plu.Name}. " +
        $"{nameof(Clip)}: {Clip.Name}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlPluClipFkEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override void FillProperties()
    {
        base.FillProperties();
        Clip.FillProperties();
        Plu.FillProperties();
    }
    
    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(SqlPluClipFkEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Clip.Equals(item.Clip) &&
        Plu.Equals(item.Plu);

    #endregion
}