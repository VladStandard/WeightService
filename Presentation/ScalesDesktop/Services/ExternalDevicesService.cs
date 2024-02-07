using Ws.Domain.Models.Enums;
using Ws.Printers;
using Ws.Printers.Common;
using Ws.Printers.Features.Tsc;
using Ws.Scales.Common;
using Ws.Scales.Main;

namespace ScalesDesktop.Services;

public class ExternalDevicesService : IDisposable
{
    public IPrinter Printer { get; private set; } = new TscPrinter("127.0.0.1", 9100);
    public IScales Scales { get; private set; } = new Scales("COM6");

    public void SetupPrinter(string ip, int port, PrinterTypeEnum type)
    {
        Printer.Dispose();
        Printer = PrinterFactory.Create(ip, port, type);
        Printer.Connect();
    }
    
    public void SetupScales()
    {
        Scales.Dispose();
        Scales = new Scales("COM6");
    }

    public void Dispose()
    {
        Scales.Dispose();
        Printer.Dispose();
        GC.SuppressFinalize(this);
    }
}