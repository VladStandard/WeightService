// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Domain.Models.Entities.Ref1c;

[DebuggerDisplay("{ToString()}")]
public class ClipEntity() : Entity1CBase(SqlEnumFieldIdentity.Uid)
{
    public virtual decimal Weight { get; set; }
    
    protected override bool CastEquals(EntityBase obj)
    {
        ClipEntity item = (ClipEntity)obj;
        return Equals(Weight, item.Weight);
    }
}