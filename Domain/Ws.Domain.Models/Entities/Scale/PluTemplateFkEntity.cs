// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Models.Entities.Scale;

[DebuggerDisplay("{ToString()}")]
public class PluTemplateFkEntity : EntityBase
{
    public virtual PluEntity Plu { get; set; } = new();
    public virtual TemplateEntity Template { get; set; } = new();

    public override string ToString() => $"{Plu} : {Template} ";

    protected override bool CastEquals(EntityBase obj)
    {
        PluTemplateFkEntity item = (PluTemplateFkEntity)obj;
        return Equals(Plu, item.Plu) && Equals(Template, item.Template);
    }
}