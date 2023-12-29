// ReSharper disable VirtualMemberCallInConstructor
namespace Ws.StorageCore.Entities.SchemaScale.PlusTemplatesFks;

[DebuggerDisplay("{ToString()}")]
public class SqlPluTemplateFkEntity : SqlEntityBase
{
    public virtual SqlPluEntity Plu { get; set; }
    public virtual SqlTemplateEntity Template { get; set; }
    
    public SqlPluTemplateFkEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Plu = new();
        Template = new();
    }

    public SqlPluTemplateFkEntity(SqlPluTemplateFkEntity item) : base(item)
    {
        Plu = new(item.Plu);
        Template = new(item.Template);
    }
    
    public override string ToString() =>
        $"{nameof(Plu)}: {Plu}. " +
        $"{nameof(Template)}: {Template}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlPluTemplateFkEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public virtual bool Equals(SqlPluTemplateFkEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Plu.Equals(item.Plu) &&
        Template.Equals(item.Template);
}