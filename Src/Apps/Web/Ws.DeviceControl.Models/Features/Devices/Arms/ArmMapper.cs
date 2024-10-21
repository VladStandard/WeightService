using Ws.DeviceControl.Models.Features.Devices.Arms.Commands;
using Ws.DeviceControl.Models.Features.Devices.Arms.Queries;

namespace Ws.DeviceControl.Models.Features.Devices.Arms;

public static class ArmMapper
{
    public static ArmUpdateDto DtoToUpdateDto(ArmDto item)
    {
        return new()
        {
            Name = item.Name,
            Type = item.Type,
            Number = item.Number,
            Counter = item.Counter,
            PcName = item.PcName,
            PrinterId = item.Printer.Id,
            WarehouseId = item.Warehouse.Id
        };
    }
}