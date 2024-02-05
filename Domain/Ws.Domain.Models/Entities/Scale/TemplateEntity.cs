// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Domain.Models.Entities.Scale;

[DebuggerDisplay("{ToString()}")]
public class TemplateEntity : EntityBase
{
    public virtual string Title { get; set; }
    public virtual string Data { get; set; }
    
    public TemplateEntity() : base(SqlEnumFieldIdentity.Id)
    {
        Title = string.Empty;
        Data = string.Empty;
    }
    
    public override string ToString() => Title;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((TemplateEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();
    
    public virtual bool Equals(TemplateEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Title, item.Title) &&
        Equals(Data, item.Data);
}