using Ws.Database.Entities.Ref.Lines;
using Ws.Database.Entities.Ref1C.Plus;
using Ws.DeviceControl.Models.Features.Devices.Arms.Queries;

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
            SystemKey = arm.SystemKey,
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
            ProductionSite = new()
            {
                Id = arm.Warehouse.ProductionSite.Id,
                Name = arm.Warehouse.ProductionSite.Name
            },
            CreateDt = arm.CreateDt,
            ChangeDt = arm.ChangeDt
        };

    public static Expression<Func<PluEntity, PluArmDto>> ToPluDto(List<Guid> plusId) =>
        plu => new()
        {
            Id = plu.Id,
            Name = plu.Name,
            Number = (ushort)plu.Number,
            IsWeight = plu.IsWeight,
            Brand = plu.Brand.Name,
            IsActive = plusId.Contains(plu.Id)
        };
}