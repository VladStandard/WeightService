using Ws.Printers.Common;
using Ws.Printers.Main.Tsc;
using Ws.Printers.Main.Zebra;
using Ws.StorageCore.Enums;

namespace Ws.Printers.Utils;

public static class PrinterFactory
{
    public static IPrinter Create(PrinterTypeEnum type) =>
        type switch {
            PrinterTypeEnum.Tsc => new TscPrinter(),
            PrinterTypeEnum.Zebra => new ZebraPrinter(),
        _ => new TscPrinter()
    };
}