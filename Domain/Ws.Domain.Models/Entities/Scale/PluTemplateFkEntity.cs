// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Models.Entities.Scale;

[DebuggerDisplay("{ToString()}")]
public class PluTemplateFkEntity() : EntityBase(SqlEnumFieldIdentity.Uid)
{
    public virtual PluEntity Plu { get; set; } = new();
    public virtual TemplateEntity Template { get; set; } = new();

    public override string ToString() =>
        $"{nameof(Plu)}: {Plu}. " +
        $"{nameof(Template)}: {Template}. ";

    public override bool Equals(object? obj)
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