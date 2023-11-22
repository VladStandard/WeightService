using Ws.Printers.Common;
using Ws.Printers.Main.Tsc;
using Ws.Printers.Utils;
using Ws.StorageCore.Enums;

namespace ScalesHybrid.Services;

public class ExternalDevicesService : IDisposable
{
    public IPrinter Printer { get; private set; }

    public ExternalDevicesService()
    {
        Printer = new TscPrinter("127.0.0.1", 9100);
    }
    
    public void SetupPrinter(string ip, int port, PrinterTypeEnum type)
    {
        Printer.Dispose();
        Printer = PrinterFactory.Create(ip, port, type).Connect();
    }

    public void Dispose()
    {
        Printer.Dispose();
    }
}