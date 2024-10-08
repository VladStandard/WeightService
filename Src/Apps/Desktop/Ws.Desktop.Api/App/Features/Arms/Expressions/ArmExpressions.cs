using Ws.Database.EntityFramework.Entities.Ref.Lines;
using Ws.Desktop.Models.Features.Arms.Output;

namespace Ws.Desktop.Api.App.Features.Arms.Expressions;


internal static class ArmExpressions
{
    public static Expression<Func<LineEntity, ArmValue>> ToDto => arm =>
        new()
        {
            Id = arm.Id,
            Counter = (uint)arm.Counter,
            Name = arm.Name,
            PcName = arm.PcName,
            Warehouse = arm.Warehouse.Name,
            Type = arm.Type,
            Printer = new()
            {
                Ip = arm.Printer.Ip,
                Name = arm.Printer.Name,
                Type = arm.Printer.Type,
            }
        };
}