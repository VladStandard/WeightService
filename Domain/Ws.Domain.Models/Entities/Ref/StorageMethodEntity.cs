// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Domain.Models.Entities.Ref;

[DebuggerDisplay("{ToString()}")]
public class StorageMethodEntity : EntityBase
{
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Zpl { get; set; } = string.Empty;

    protected override bool CastEquals(EntityBase obj)
    {
        StorageMethodEntity item = (StorageMethodEntity)obj;
        return Equals(Zpl, item.Zpl) && Equals(Name, item.Name);
    }
}