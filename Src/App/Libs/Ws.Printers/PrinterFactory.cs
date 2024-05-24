using System.Net;
using Ws.Domain.Models.Enums;
using Ws.Printers.Common;
using Ws.Printers.Features.Tsc;
using Ws.Printers.Features.Zebra;

namespace Ws.Printers;

public static class PrinterFactory
{
    public static IPrinter Create(IPAddress ip, int port, PrinterTypes types) =>
        types switch
        {
            PrinterTypes.Tsc => new TscPrinter(ip, port),
            PrinterTypes.Zebra => new ZebraPrinter(ip, port),
            _ => new TscPrinter(ip, port)
        };
}