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

    protected override bool CastEquals(EntityBase obj)
    {
        TemplateResourceEntity item = (TemplateResourceEntity)obj;
        return Equals(Data, item.Data);
    }
}