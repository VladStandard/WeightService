using Ws.Database.EntityFramework;
using Ws.Desktop.Api.App.Features.Arms.Common;
using Ws.Desktop.Models.Features.Arms.Output;

namespace Ws.Desktop.Api.App.Features.Arms.Impl;

public class ArmService : IArmService
{
    public ArmValue? GetByName(string armName)
    {
        using var context = new WsDbContext();
        return context.Lines
            .Where(i => i.PcName == armName)
            .Select(i => new ArmValue
            {
                Id = i.Id,
                Counter = (uint)Math.Abs(i.Counter),
                Name = i.Name,
                PcName = i.PcName,
                Warehouse = i.Warehouse.Name,
                Type = i.Type,
                Printer = new()
                {
                    Ip = i.Printer.Ip,
                    Name = i.Printer.Name,
                    Type = i.Printer.Type,
                }
            })
            .FirstOrDefault();
    }
}