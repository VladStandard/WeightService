using Ws.Printers.Common;
using Ws.Printers.Main;
using Ws.Printers.Utils;
using Ws.Scales.Common;
using Ws.Scales.Main;
using Ws.StorageCore.Enums;

namespace ScalesHybrid.Services;

public class ExternalDevicesService : IDisposable
{
    public IPrinter Printer { get; private set; }
    public IScales Scales { get; private set; }
    
    public ExternalDevicesService()
    {
        Printer = new TscPrinter("127.0.0.1", 9100);
        Scales = new Scales("COM6");
    }
    
    public void SetupPrinter(string ip, int port, PrinterTypeEnum type)
    {
        Printer.Dispose();
        Printer = PrinterFactory.Create(ip, port, type).Connect();
    }
    
    public void SetupScales(string comPort)
    {
        Scales.Dispose();
        Scales = new Scales(comPort);
    }

    public void Dispose()
    {
        Scales.Dispose();
        Printer.Dispose();
    }
}