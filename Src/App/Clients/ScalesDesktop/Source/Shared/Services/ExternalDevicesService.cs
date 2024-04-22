using System.Net;
using Ws.Domain.Models.Enums;
using Ws.Printers;
using Ws.Printers.Common;
using Ws.Scales.Common;
using Ws.Scales.Main;

namespace ScalesDesktop.Source.Shared.Services;

public class ExternalDevicesService : IDisposable
{
    public IPrinter Printer { get; private set; } =
        PrinterFactory.Create(IPAddress.Parse("127.0.0.1"), 9100, PrinterTypeEnum.Tsc);

    public IScales Scales { get; private set; } = new Scales("COM6");

    public void SetupPrinter(IPAddress ip, int port, PrinterTypeEnum type)
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