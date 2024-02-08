// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Domain.Models.Entities.Ref;

[Serializable]
[DebuggerDisplay("{ToString()}")]
public class StorageMethodEntity() : EntityBase(SqlEnumFieldIdentity.Uid)
{
    public virtual string Zpl { get; set; } = string.Empty;

    protected override bool CastEquals(EntityBase obj)
    {
        StorageMethodEntity item = (StorageMethodEntity)obj;
        return Equals(Zpl, item.Zpl);
    }
}