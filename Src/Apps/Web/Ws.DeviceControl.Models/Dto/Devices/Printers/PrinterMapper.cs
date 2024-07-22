using Ws.DeviceControl.Models.Dto.Devices.Printers.Commands.Update;
using Ws.DeviceControl.Models.Dto.Devices.Printers.Queries;

namespace Ws.DeviceControl.Models.Dto.Devices.Printers;

public static class PrinterMapper
{
    public static PrinterUpdateDto DtoToUpdateDto(PrinterDto item)
    {
        return new()
        {
            Name = item.Name,
            Ip = item.Ip,
            Type = item.Type
        };
    }
}