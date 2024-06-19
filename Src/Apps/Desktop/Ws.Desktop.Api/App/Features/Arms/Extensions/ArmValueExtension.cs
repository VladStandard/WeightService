using Ws.Database.EntityFramework.Entities.Ref.Lines;
using Ws.Desktop.Models.Features.Arms.Output;

namespace Ws.Desktop.Api.App.Features.Arms.Extensions;

internal static class ArmValueExtensions
{
    public static IQueryable<ArmValue> ToArmValue(this IQueryable<LineEntity> query)
    {
        return query.Select(i => new ArmValue
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
        });
    }
}