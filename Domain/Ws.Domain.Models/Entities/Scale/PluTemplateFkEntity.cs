// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.SchemaScale;

namespace Ws.Domain.Models.Entities.Scale;

[DebuggerDisplay("{ToString()}")]
public class PluTemplateFkEntity : EntityBase
{
    public virtual PluEntity Plu { get; set; }
    public virtual TemplateEntity Template { get; set; }
    
    public PluTemplateFkEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Plu = new();
        Template = new();
    }

    public PluTemplateFkEntity(PluTemplateFkEntity item) : base(item)
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
        return Equals((PluTemplateFkEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public virtual bool Equals(PluTemplateFkEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Plu.Equals(item.Plu) &&
        Template.Equals(item.Template);
}