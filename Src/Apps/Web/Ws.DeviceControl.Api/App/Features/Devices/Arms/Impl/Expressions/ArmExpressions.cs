using Ws.Database.EntityFramework.Entities.Ref.Lines;
using Ws.DeviceControl.Models.Dto.Devices.Arms.Queries;

namespace Ws.DeviceControl.Api.App.Features.Devices.Arms.Impl.Expressions;

public static class ArmExpressions
{
    public static Expression<Func<LineEntity, ArmDto>> ToDto =>
        arm => new()
        {
            Id = arm.Id,
            Name = arm.Name,
            Version = arm.Version,
            Type = arm.Type,
            Number = arm.Number,
            Counter = arm.Counter,
            PcName = arm.PcName,
            Printer = new()
            {
                Id = arm.Printer.Id,
                Name = arm.Printer.Name
            },
            Warehouse = new()
            {
                Id = arm.Warehouse.Id,
                Name = arm.Warehouse.Name
            },
            CreateDt = arm.CreateDt,
            ChangeDt = arm.ChangeDt
        };
}