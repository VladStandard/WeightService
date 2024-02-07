using Ws.Domain.Models.Enums;
using Ws.Printers.Common;
using Ws.Printers.Features.Tsc;
using Ws.Printers.Features.Zebra;

namespace Ws.Printers;

public static class PrinterFactory
{
    public static IPrinter Create(string ip, int port, PrinterTypeEnum type) =>
        type switch {
            PrinterTypeEnum.Tsc => new TscPrinter(ip, port),
            PrinterTypeEnum.Zebra => new ZebraPrinter(ip, port),
        _ => new TscPrinter(ip, port)
    };
}