// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Users;

namespace Ws.Domain.Models.Entities.Print;

[DebuggerDisplay("{ToString()}")]
public class Pallet : EntityBase
{
    public virtual PalletMan PalletMan { get; set; } = new();
    public virtual Arm Arm { get; set; } = new();
    public virtual string Barcode { get; set; } = string.Empty;
    public virtual string Number { get; set; }  = string.Empty;
    public virtual int Counter { get; set; }
    public virtual DateTime ProdDt { get; set; }
    public virtual decimal Weight { get; set; }

    protected override bool CastEquals(EntityBase obj)
    {
        Pallet item = (Pallet)obj;
        return Equals(Counter, item.Counter) &&
               Equals(PalletMan, item.PalletMan) &&
               Equals(Barcode, item.Barcode);
    }
}