// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Domain.Models.Entities.Scale;

[DebuggerDisplay("{ToString()}")]
public class TemplateEntity() : EntityBase(SqlEnumFieldIdentity.Id)
{
    public virtual string Title { get; set; } = string.Empty;
    public virtual string Data { get; set; } = string.Empty;
    public override string ToString() => Title;

    protected override bool CastEquals(EntityBase obj)
    {
        TemplateEntity item = (TemplateEntity)obj;
        return Equals(Title, item.Title) && Equals(Data, item.Data);
    }
}