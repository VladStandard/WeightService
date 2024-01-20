// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;

namespace Ws.Domain.Models.Entities.Ref;

[Serializable]
[DebuggerDisplay("{ToString()}")]
public class StorageMethodEntity : EntityBase
{
    public virtual string Zpl { get; set; }
    
    public StorageMethodEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Zpl = string.Empty;
    }

    public StorageMethodEntity(StorageMethodEntity item) : base(item)
    {
        Zpl = item.Zpl;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((StorageMethodEntity)obj);
    }

    public override int GetHashCode() => IdentityValueUid.GetHashCode();
    
    public virtual bool Equals(StorageMethodEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Zpl, item.Zpl);
}