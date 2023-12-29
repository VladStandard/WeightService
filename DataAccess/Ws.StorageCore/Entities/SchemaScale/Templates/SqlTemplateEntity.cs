// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
namespace Ws.StorageCore.Entities.SchemaScale.Templates;

[DebuggerDisplay("{ToString()}")]
public class SqlTemplateEntity : SqlEntityBase
{
    public virtual string CategoryId { get; set; } 
    public virtual string Title { get; set; }
    public virtual string Data { get; set; }
    
    public SqlTemplateEntity() : base(SqlEnumFieldIdentity.Id)
    {
        CategoryId = string.Empty;
        Title = string.Empty;
        Data = string.Empty;
    }

    public SqlTemplateEntity(SqlTemplateEntity item) : base(item)
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
        return Equals((SqlTemplateEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();
    
    public virtual bool Equals(SqlTemplateEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(CategoryId, item.CategoryId) &&
        Equals(Title, item.Title) &&
        Equals(Data, item.Data);
}