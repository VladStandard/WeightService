// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Models.Entities.Ref;

[DebuggerDisplay("{ToString()}")]
public class PluLineEntity : EntityBase
{
    public virtual PluEntity Plu { get; set; } = new();
    public virtual LineEntity Line { get; set; } = new();
    
    protected override bool CastEquals(EntityBase obj)
    {
        PluLineEntity item = (PluLineEntity)obj;
        return Equals(Plu, item.Plu) && Equals(Line, item.Line);
    }
}