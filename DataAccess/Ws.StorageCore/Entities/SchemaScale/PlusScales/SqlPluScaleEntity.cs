namespace Ws.StorageCore.Entities.SchemaScale.PlusScales;

[DebuggerDisplay("{ToString()}")]
public class SqlPluScaleEntity : SqlEntityBase
{
    public virtual SqlPluEntity Plu { get; set; }
    public virtual SqlLineEntity Line { get; set; }
    
    public SqlPluScaleEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Plu = new();
        Line = new();
    }
    
    public SqlPluScaleEntity(SqlPluScaleEntity item) : base(item)
    {
        Plu = new(item.Plu);
        Line = new(item.Line);
    }

    public override string ToString() => $"{Plu} | {Line}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlPluScaleEntity)obj);
    }

    public override int GetHashCode() => (Plu, Scale: Line).GetHashCode();

    public override void FillProperties()
    {
        base.FillProperties();
        Plu.FillProperties();
        Line.FillProperties();
    }
    
    public virtual bool Equals(SqlPluScaleEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Plu, item.Plu) &&
        Equals(Line, item.Line) &&
        Plu.Equals(item.Plu) &&
        Line.Equals(item.Line);
}
