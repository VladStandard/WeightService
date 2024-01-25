// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Enums;

namespace Ws.Domain.Models.Entities.Ref;

[DebuggerDisplay("{ToString()}")]
public class LineEntity : EntityBase
{
    public virtual string PcName { get; set; }
    public virtual WarehouseEntity Warehouse { get; set; }
    public virtual PrinterEntity Printer { get; set; }
    public virtual int Number { get; set; }
    public override string DisplayName => IsNew ? string.Empty : $"{Name}";
    private int _counter;

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

    public LineEntity(LineEntity item) : base(item)
    {
        Warehouse = new(item.Warehouse);
        Printer = new(item.Printer);
        Number = item.Number;
        Counter = item.Counter;
        Version = item.Version;
        PcName = item.PcName;
        Type = item.Type;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((LineEntity)obj);
    }
    
    public virtual bool Equals(LineEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Number, item.Number) &&
        Equals(Counter, item.Counter) &&
        Equals(PcName, item.PcName) &&
        Equals(Type, item.Type) &&
        Warehouse.Equals(item.Warehouse) &&
        Printer.Equals(item.Printer) &&
        Version.Equals(item.Version);
    
    public override int GetHashCode() => base.GetHashCode();
    public override string ToString() => $"{IdentityValueId} | {Name}";
}