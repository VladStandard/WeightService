// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Models.Entities.Print;

[DebuggerDisplay("{ToString()}")]
public class PalletEntity : EntityBase
{
    public virtual PalletManEntity PalletMan { get; set; } = new();
    public virtual string Barcode { get; set; } = string.Empty;
    public virtual int Counter { get; protected set; }
    public virtual DateTime ProdDt { get; set; }
    public virtual decimal Weight { get; set; }

    protected override bool CastEquals(EntityBase obj)
    {
        PalletEntity item = (PalletEntity)obj;
        return Equals(Counter, item.Counter) &&
               Equals(PalletMan, item.PalletMan) &&
               Equals(Barcode, item.Barcode);
    }
}