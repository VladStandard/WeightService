// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Models;
using Ws.Shared.Utils;

namespace Ws.Domain.Models.Entities.Scale;

[DebuggerDisplay("{ToString()}")]
public class TemplateResourceEntity : EntityBase
{
    public virtual FieldBinaryModel Data { get; set; }
    public virtual byte[] DataValue { get => Data.Value ?? Array.Empty<byte>(); set => Data.Value = value; }
    
    public TemplateResourceEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Data = new();
    }
    
    public TemplateResourceEntity(TemplateResourceEntity item) : base(item)
    {
        Data = new(item.Data);
        DataValue = DataUtils.ByteClone(item.DataValue);
    }
    
    public override string ToString() => $"{nameof(Name)}: {Name}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((TemplateResourceEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public virtual bool Equals(TemplateResourceEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Data.Equals(item.Data);
}