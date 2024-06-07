using TscZebra.Plugin.Abstractions.Enums;
using Ws.Database.EntityFramework;
using Ws.Desktop.Api.App.Features.Arms.Common;
using Ws.Desktop.Models.Common;
using Ws.Desktop.Models.Features.Arms.Output;

namespace Ws.Desktop.Api.App.Features.Arms.Impl;

public class ArmService : IArmService
{
    public OutputDto<Arm>? GetByName(string armName)
    {
        using var context = new WsDbContext();
        Arm? arm = context.Lines
            .Where(i => i.PcName == armName)
            .Select(i => new Arm
            {
                Id = i.Id,
                Counter = (uint)Math.Abs(i.Counter),
                Name = i.Name,
                PcName = i.PcName,
                Warehouse = i.Warehouse.Name,
                Printer = new()
                {
                    Ip = i.Printer.Ip,
                    Name = i.Printer.Name,
                    Type = i.Printer.Type,
                }
            })
            .FirstOrDefault();

        return arm is not null ? new (arm) : null;
    }
}