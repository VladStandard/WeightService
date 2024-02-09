// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;
using Ws.Domain.Models.Enums;

namespace Ws.Domain.Models.Entities.Ref;

[DebuggerDisplay("{ToString()}")]
public class LineEntity : EntityBase
{
    private int _counter;

    public virtual string PcName { get; set; }
    public virtual WarehouseEntity Warehouse { get; set; }
    public virtual PrinterEntity Printer { get; set; }
    public virtual int Number { get; set; }
    public virtual int Counter { get => _counter; set { _counter = value > 1_000_000 ? 1 : value; } }
    public virtual string Version { get; set; } = string.Empty;
    public virtual LineTypeEnum Type { get; set; }

    public LineEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Printer = new();
        Warehouse = new();
        PcName = string.Empty;
        Version = string.Empty;
        Number = 0;
        Counter = 0;
        Type = LineTypeEnum.Tablet;
    }

    protected override bool CastEquals(EntityBase obj)
    {
        LineEntity item = (LineEntity)obj;
        return Equals(Number, item.Number) &&
               Equals(Counter, item.Counter) &&
               Equals(PcName, item.PcName) &&
               Equals(Type, item.Type) &&
               Equals(Warehouse, item.Warehouse) &&
               Equals(Printer, item.Printer) &&
               Equals(Version, item.Version);
    }
}