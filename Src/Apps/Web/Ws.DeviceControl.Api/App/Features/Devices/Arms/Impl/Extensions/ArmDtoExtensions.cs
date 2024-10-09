using Ws.Database.Entities.Ref.Lines;
using Ws.Database.Entities.Ref.Printers;
using Ws.Database.Entities.Ref.Warehouses;
using Ws.DeviceControl.Models.Features.Devices.Arms.Commands.Create;
using Ws.DeviceControl.Models.Features.Devices.Arms.Commands.Update;

namespace Ws.DeviceControl.Api.App.Features.Devices.Arms.Impl.Extensions;

internal static class ArmDtoExtensions
{
    public static LineEntity ToEntity(this ArmCreateDto dto, WarehouseEntity warehouse, PrinterEntity printer)
    {
        return new()
        {
            Name = dto.Name,
            Number = dto.Number,
            PcName = dto.PcName,
            Type = dto.Type,
            Printer = printer,
            Warehouse = warehouse,
            Counter = 1
        };
    }

    public static void UpdateEntity(this ArmUpdateDto dto, LineEntity entity, PrinterEntity printer, WarehouseEntity warehouse)
    {
        entity.Name = dto.Name;
        entity.Type = dto.Type;
        entity.Printer = printer;
        entity.Number = dto.Number;
        entity.PcName = dto.PcName;
        entity.Counter = dto.Counter;
        entity.Warehouse = warehouse;
    }
}