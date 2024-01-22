using ScalesHybrid.Models;
using ScalesHybrid.Utils;
using Ws.Services.Features.Line;
using Ws.Services.Features.Plu;
using Ws.StorageCore.Entities.SchemaRef.Lines;
using Ws.StorageCore.Entities.SchemaRef.Printers;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;
using Ws.StorageCore.Entities.SchemaScale.Templates;
using Ws.StorageCore.Helpers;

namespace ScalesHybrid.Services;

public class LineContext: IDisposable
{
    private ILineService LineService { get; }
    private ExternalDevicesService ExternalDevices { get; }
    
    public SqlLineEntity Line { get; private set; } = new();
    public SqlPrinterEntity PrinterEntity { get; private set; } = new();
    public event Action? OnLabelChanged;

    public LineContext(ILineService lineService, ExternalDevicesService externalDevices)
    {
        LineService = lineService;
        ExternalDevices = externalDevices;
        InitializeData();
    }
    
    private void InitializeData()
    {
        Line = LineService.GetCurrentLine();

        if (Line.IsExists)
        {
            Line.Version = VersionTracking.CurrentVersion;
            SqlCoreHelper.Instance.Update(Line);
        }
        
        PrinterEntity = Line.Printer;
        ExternalDevices.SetupPrinter(Line.Printer.Ip, Line.Printer.Port, Line.Printer.Type);
        ExternalDevices.SetupScales(Line.ComPort);
    }

    public void ResetLine() {
        ExternalDevices.Scales.Disconnect();
        Line = LineService.GetCurrentLine();
        PrinterEntity = Line.Printer;
        ExternalDevices.SetupPrinter(PrinterEntity.Ip, PrinterEntity.Port, PrinterEntity.Type);
        OnLabelChanged?.Invoke();
    }

    public void DisconnectScale() => ExternalDevices.Scales.Disconnect();

    public void ConnectScale() => ExternalDevices.Scales.Connect();

    public void RequestScale() => ExternalDevices.Scales.SendGetWeight();

    public void Dispose()
    {
        ExternalDevices.Dispose();
        GC.SuppressFinalize(this);
    }
}