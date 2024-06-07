// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;

namespace Ws.Domain.Models.Entities.Print;

[DebuggerDisplay("{ToString()}")]
public class StorageMethod : EntityBase
{
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Zpl { get; set; } = string.Empty;

    protected override bool CastEquals(EntityBase obj)
    {
        StorageMethod item = (StorageMethod)obj;
        return Equals(Zpl, item.Zpl) && Equals(Name, item.Name);
    }
}