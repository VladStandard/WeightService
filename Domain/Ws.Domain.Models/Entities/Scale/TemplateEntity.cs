// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Models.Common;

namespace Ws.Domain.Models.Entities.Scale;

[DebuggerDisplay("{ToString()}")]
public class TemplateEntity : EntityBase
{
    public virtual string CategoryId { get; set; } 
    public virtual string Title { get; set; }
    public virtual string Data { get; set; }
    
    public TemplateEntity() : base(SqlEnumFieldIdentity.Id)
    {
        CategoryId = string.Empty;
        Title = string.Empty;
        Data = string.Empty;
    }

    public TemplateEntity(TemplateEntity item) : base(item)
    {
        CategoryId = item.CategoryId;
        Title = item.Title;
        Data = item.Data;
    }
    
    public override string ToString() => $"{CategoryId} | {Title}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((TemplateEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();
    
    public virtual bool Equals(TemplateEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(CategoryId, item.CategoryId) &&
        Equals(Title, item.Title) &&
        Equals(Data, item.Data);
}