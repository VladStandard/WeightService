// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;

namespace Ws.Domain.Models.Entities.Ref1c.Plus;

[DebuggerDisplay("{ToString()}")]
public class PluCharacteristic : EntityBase
{
    public virtual Box Box { get; set; } = new();
    public virtual Guid PluUid { get; set; }
    public virtual short BundleCount { get; set; }
    public virtual string Name { get; set; } = string.Empty;

    protected override bool CastEquals(EntityBase obj)
    {
        PluCharacteristic item = (PluCharacteristic)obj;
        return Equals(Box, item.Box) &&
               Equals(Name, item.Name) &&
               Equals(PluUid, item.PluUid) &&
               Equals(BundleCount, item.BundleCount);
    }
}