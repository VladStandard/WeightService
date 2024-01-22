using Ws.Domain.Models.Enums;
using Ws.Printers.Common;
using Ws.Printers.Main;
using Ws.Printers.Utils;
using Ws.Scales.Common;
using Ws.Scales.Main;

namespace ScalesHybrid.Services;

public class ExternalDevicesService : IDisposable
{
    public IPrinter Printer { get; private set; } = new TscPrinter("127.0.0.1", 9100);
    public IScales Scales { get; private set; } = new Scales("COM6");

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
        GC.SuppressFinalize(this);
    }
}