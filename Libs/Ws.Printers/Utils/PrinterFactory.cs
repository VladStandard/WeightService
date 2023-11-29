using Ws.Printers.Common;
using Ws.Printers.Main;
using Ws.StorageCore.Enums;

namespace Ws.Printers.Utils;

public static class PrinterFactory
{
    public static IPrinter Create(string ip, int port, PrinterTypeEnum type) =>
        type switch {
            PrinterTypeEnum.Tsc => new TscPrinter(ip, port),
            PrinterTypeEnum.Zebra => new ZebraPrinter(ip, port),
        _ => new TscPrinter(ip, port)
    };
}