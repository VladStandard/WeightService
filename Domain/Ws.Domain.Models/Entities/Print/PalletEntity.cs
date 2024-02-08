// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Models.Entities.Print;

[DebuggerDisplay("{ToString()}")]
public class PalletEntity() : EntityBase(SqlEnumFieldIdentity.Uid)
{
    public virtual PalletManEntity PalletMan { get; set; } = new();
    public virtual string Barcode { get; set; } = string.Empty;
    public virtual int Counter { get; set; }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PalletEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public virtual bool Equals(PalletEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Counter, item.Counter) &&
        Equals(PalletMan, item.PalletMan) &&
        Equals(Barcode, item.Barcode);
}