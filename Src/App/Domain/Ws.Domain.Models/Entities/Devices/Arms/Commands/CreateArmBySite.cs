using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Enums;

namespace Ws.Domain.Models.Entities.Devices.Arms.Commands;

public sealed class CreateArmBySite {
    public int Number { get; set; }
    public ArmTypes Type { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PcName { get; set; } = string.Empty;
    public Printer Printer { get; set; } = new();
    public Warehouse Warehouse { get; set; } = new();

}