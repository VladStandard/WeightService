namespace WsStorageCore.Entities.SchemaScale.PlusClipsFks;

[DebuggerDisplay("{ToString()}")]
public class WsSqlPluClipFkEntity : WsSqlEntityBase
{
    #region Public and private fields, properties, constructor

    public virtual WsSqlClipEntity Clip { get; set; }
    public virtual WsSqlPluEntity Plu { get; set; }
    
    public WsSqlPluClipFkEntity() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Clip = new();
        Plu = new();

    }
    
    public WsSqlPluClipFkEntity(WsSqlPluClipFkEntity item) : base(item)
    {
        Clip = new(item.Clip);
        Plu = new(item.Plu);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Plu)}: {Plu.Name}. " +
        $"{nameof(Clip)}: {Clip.Name}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPluClipFkEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Clip.EqualsDefault() &&
        Plu.EqualsDefault();

    public override void FillProperties()
    {
        base.FillProperties();
        Clip.FillProperties();
        Plu.FillProperties();
    }
    
    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPluClipFkEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Clip.Equals(item.Clip) &&
        Plu.Equals(item.Plu);

    #endregion
}