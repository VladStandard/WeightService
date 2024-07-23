using Ws.DeviceControl.Models.Dto.Devices.Arms.Commands.Update;
using Ws.DeviceControl.Models.Dto.Devices.Arms.Queries;

namespace Ws.DeviceControl.Models.Dto.Devices.Arms;

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
            WarehouseId = item.Printer.Id
        };
    }
}