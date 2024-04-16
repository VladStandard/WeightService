// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;

namespace Ws.Domain.Models.Entities.Ref1c;

[DebuggerDisplay("{ToString()}")]
public class CharacteristicEntity : EntityBase
{
    public virtual BoxEntity Box { get; set; } = new();
    public virtual Guid PluUid { get; set; }
    public virtual short BundleCount { get; set; }
    public virtual string Name { get; set; } = string.Empty;

    protected override bool CastEquals(EntityBase obj)
    {
        CharacteristicEntity item = (CharacteristicEntity)obj;
        return Equals(Box, item.Box) &&
               Equals(Name, item.Name) &&
               Equals(PluUid, item.PluUid) &&
               Equals(BundleCount, item.BundleCount);
    }
}