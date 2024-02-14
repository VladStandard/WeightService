// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;
using Ws.Domain.Models.Enums;

namespace Ws.Domain.Models.Entities.Ref;

[DebuggerDisplay("{ToString()}")]
public class PrinterEntity : EntityBase
{
    public virtual string Ip { get; set; } = string.Empty;
    public virtual short Port { get; set; } = 9100;
    public virtual PrinterTypeEnum Type { get; set; } = PrinterTypeEnum.Tsc;
    public virtual string Name { get; set; } = string.Empty;
    public virtual string DisplayName => $"{Name} | {Ip}";

    protected override bool CastEquals(EntityBase obj)
    {
        PrinterEntity item = (PrinterEntity)obj;
        return Equals(Ip, item.Ip) && Equals(Port, item.Port) && Equals(Type, item.Type) && Equals(Name, item.Name);
    }
}