// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Enums;

namespace Ws.Domain.Models.Entities.Ref;

[DebuggerDisplay("{ToString()}")]
public class PrinterEntity : EntityBase
{
    public virtual string Ip { get; set; }
    public virtual short Port { get; set; }
    public virtual PrinterTypeEnum Type { get; set; }
    public override string DisplayName => $"{Name} | {Ip}";

    public PrinterEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Ip = string.Empty;
        Port = 9100;
        Type = PrinterTypeEnum.Tsc;
    }

    public override string ToString() => $"{nameof(Type)}: {Type}.";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PrinterEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();
    
    public virtual bool Equals(PrinterEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Ip, item.Ip) &&
        Equals(Port, item.Port) &&
        Equals(Type, item.Type);
}