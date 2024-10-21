using Ws.DeviceControl.Models.Features.Devices.Printers.Commands;
using Ws.DeviceControl.Models.Features.Devices.Printers.Queries;

namespace Ws.DeviceControl.Models.Features.Devices.Printers;

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