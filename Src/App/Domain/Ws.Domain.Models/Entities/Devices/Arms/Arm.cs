// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Entities.Devices.Arms.Commands;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Enums;

namespace Ws.Domain.Models.Entities.Devices.Arms;

[DebuggerDisplay("{ToString()}")]
public class Arm : EntityBase
{
    private int _counter;
    public virtual int Counter { get => _counter; set => _counter = value > 1_000_000 ? 1 : value; }

    public virtual int Number { get; set; }
    public virtual string Name { get; set; } = string.Empty;
    public virtual string PcName { get; set; } = string.Empty;
    public virtual string Version { set; get; } = string.Empty;

    public virtual Printer Printer { get; set; } = new();
    public virtual Warehouse Warehouse { get; set; } = new();
    public virtual ArmTypes Type { get; set; } = ArmTypes.Tablet;

    #region Constructors

    public Arm() { } // DEFAULT

    public Arm(CreateArmBySite command)
    {
        Number = command.Number;
        Name = command.Name;
        PcName = command.PcName;
        Type = command.Type;
        Printer = command.Printer;
        Warehouse = command.Warehouse;
    }

    #endregion

    protected override bool CastEquals(EntityBase obj)
    {
        Arm item = (Arm)obj;
        return Equals(Name, item.Name) &&
               Equals(Number, item.Number) &&
               Equals(Counter, item.Counter) &&
               Equals(PcName, item.PcName) &&
               Equals(Type, item.Type) &&
               Equals(Warehouse, item.Warehouse) &&
               Equals(Printer, item.Printer) &&
               Equals(Version, item.Version);
    }
}