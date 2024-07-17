// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.ValueTypes;

namespace Ws.Domain.Models.Entities.Users;

[DebuggerDisplay("{ToString()}")]
public class PalletMan : EntityBase
{
    public virtual Guid Uid1C { get; set; }
    public virtual Warehouse Warehouse { get; set; } = new();
    public virtual Fio Fio { get; set; } = new();
    public virtual string Password { get; set; } = string.Empty;

    protected override bool CastEquals(EntityBase obj)
    {
        PalletMan item = (PalletMan)obj;
        return Equals(Fio, item.Fio)
               && Equals(Password, item.Password)
               && Equals(Uid1C, item.Uid1C)
               && Equals(Warehouse, item.Warehouse);
    }

    public override string ToString() => Fio.DisplayShort;
}