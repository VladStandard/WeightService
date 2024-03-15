// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using System.Net;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Enums;

namespace Ws.Domain.Models.Entities.Ref;

[DebuggerDisplay("{ToString()}")]
public class PrinterEntity : EntityBase
{
    public virtual IPAddress Ip { get; set; } = IPAddress.Parse("127.0.0.1");
    public virtual short Port { get; set; } = 9100;
    public virtual ProductionSiteEntity ProductionSite { get; set; } = new();
    public virtual PrinterTypeEnum Type { get; set; } = PrinterTypeEnum.Tsc;
    public virtual string Name { get; set; } = string.Empty;
    public virtual string DisplayName => $"{Name} | {Ip}";

    protected override bool CastEquals(EntityBase obj)
    {
        PrinterEntity item = (PrinterEntity)obj;
        return Equals(Ip, item.Ip) && 
               Equals(Port, item.Port) && 
               Equals(Type, item.Type) && 
               Equals(Name, item.Name) && 
               Equals(ProductionSite, item.ProductionSite);
    }
}