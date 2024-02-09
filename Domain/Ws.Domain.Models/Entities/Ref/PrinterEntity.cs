// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;
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

    protected override bool CastEquals(EntityBase obj)
    {
        PrinterEntity item = (PrinterEntity)obj;
        return Equals(Ip, item.Ip) && Equals(Port, item.Port) && Equals(Type, item.Type);
    }
}