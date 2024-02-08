// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;
using Ws.Domain.Models.Models;

namespace Ws.Domain.Models.Entities.Scale;

[DebuggerDisplay("{ToString()}")]
public class TemplateResourceEntity() : EntityBase(SqlEnumFieldIdentity.Uid)
{
    public virtual FieldBinaryModel Data { get; set; } = new();
    public virtual byte[] DataValue { get => Data.Value ?? Array.Empty<byte>(); set => Data.Value = value; }

    public override bool Equals(object? obj)
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