// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using Ws.StorageCore.Entities.SchemaRef.Hosts;
using Ws.StorageCore.Entities.SchemaRef.Printers;
using Ws.StorageCore.Entities.SchemaRef.WorkShops;

namespace Ws.StorageCore.Entities.SchemaRef.Lines;

[DebuggerDisplay("{ToString()}")]
public class SqlLineEntity : SqlEntityBase
{
    public virtual SqlHostEntity Host { get; set; }
    public virtual SqlWorkShopEntity WorkShop { get; set; }
    public virtual SqlPrinterEntity Printer { get; set; }
    public virtual string ComPort { get; set; }
    public virtual int Number { get; set; }
    public override string DisplayName => IsNew ? string.Empty : $"{Name}";
    private int _counter;

    public virtual int Counter { get => _counter; set { _counter = value > 1_000_000 ? 1 : value; } }
    public virtual string Version { get; set; } = string.Empty;

    public SqlLineEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Version = string.Empty;
        WorkShop = new();
        Host = new();
        Printer = new();
        Number = 0;
        Counter = 0;
        ComPort = "COM6";
    }

    public SqlLineEntity(SqlLineEntity item) : base(item)
    {
        Host = new(item.Host);
        WorkShop = new(item.WorkShop);
        Printer = new(item.Printer);
        ComPort = item.ComPort;
        Number = item.Number;
        Counter = item.Counter;
        Version = item.Version;
    }

    public override string ToString() => $"{IdentityValueId} | {Name}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlLineEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();
    
    public virtual bool Equals(SqlLineEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(ComPort, item.ComPort) &&
        Equals(Number, item.Number) &&
        Equals(Counter, item.Counter) &&
        WorkShop.Equals(item.WorkShop) &&
        Host.Equals(item.Host) &&
        Printer.Equals(item.Printer) &&
        Version.Equals(item.Version);
}