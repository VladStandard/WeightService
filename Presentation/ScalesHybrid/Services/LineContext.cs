using ScalesHybrid.Models;
using ScalesHybrid.Utils;
using Ws.Services.Services.Host;
using Ws.Services.Services.Line;
using Ws.Services.Services.Plu;
using Ws.StorageCore.Entities.SchemaRef.Hosts;
using Ws.StorageCore.Entities.SchemaRef.Printers;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;
using Ws.StorageCore.Entities.SchemaScale.Scales;
using Ws.StorageCore.Entities.SchemaScale.Templates;

namespace ScalesHybrid.Services;

public class LineContext
{
    public SqlHostEntity Host { get; private set; }
    public SqlLineEntity Line { get; private set; }
    public SqlPluEntity Plu { get; private set; }
    public SqlPrinterEntity PrinterEntity { get; private set; }
    public SqlTemplateEntity PluTemplate { get; private set; }
    public SqlPluNestingFkEntity PluNesting { get; set; }
    public WeightKneadingModel KneadingModel { get; set; }
    public IEnumerable<SqlLineEntity> LineEntities { get; set; }
    public IEnumerable<SqlPluEntity> PluEntities { get; set; }
    public IEnumerable<SqlPluNestingFkEntity> PluNestingEntities { get; set; }
    public event Action OnStateChanged;
    
    private IHostService HostService { get; }
    private ILineService LineService { get; }
    private IPluService PluService { get; }
    private ExternalDevicesService ExternalDevices { get; }
    
    private Timer Timer { get; set; }

    public LineContext(IHostService hostService, ILineService lineService, IPluService pluService, ExternalDevicesService externalDevices)
    {
        HostService = hostService;
        LineService = lineService;
        PluService = pluService;
        ExternalDevices = externalDevices;
        InitData();
    }

    public void ChangeLine(SqlLineEntity sqlLineEntity)
    {
        if (Line.Equals(sqlLineEntity)) return;
        Line = sqlLineEntity;
        PluEntities = GetPlus();
        Plu = new();
        PluTemplate = new();
        PluNesting = new();
        ExternalDevices.Scales.Disconnect();
        NotifyStateChanged();
    }

    public void ResetLine() {
        SqlLineEntity newLine = HostService.GetLineByHost(Host);
        PrinterEntity = newLine.Printer;
        ChangeLine(newLine);
    }
    

    public async Task ChangePlu(SqlPluEntity sqlPluEntity)
    {
        if (Plu.Equals(sqlPluEntity)) return;
        Plu = sqlPluEntity;
        PluTemplate = PluService.GetPluTemplate(Plu);
        PluNestingEntities = await Task.Run(GetPluNestings);
        PluNesting = PluNestingEntities.FirstOrDefault(item => item.IsDefault) ?? new();
        if (Plu.IsCheckWeight)
            ExternalDevices.Scales.Connect();
        else
        {
            ExternalDevices.Scales.Disconnect();
        }
        NotifyStateChanged();
    }

    public void ChangePluNesting(SqlPluNestingFkEntity sqlPluNestingEntity)
    {
        if (PluNesting.Equals(sqlPluNestingEntity)) return;
        PluNesting = sqlPluNestingEntity;
        NameFormatting.GetPluNestingFormattedName(sqlPluNestingEntity);
        NotifyStateChanged();
    }

    private IEnumerable<SqlPluEntity> GetPlus() => LineService.GetLinePlus(Line);

    private IEnumerable<SqlPluNestingFkEntity> GetPluNestings() => PluService.GetPluNesting(Plu);

    private void InitData()
    {
        Host = HostService.GetCurrentHostOrCreate();
        Line = HostService.GetLineByHost(Host);
        PrinterEntity = Line.Printer;
        LineEntities = LineService.GetLinesByWorkshop(Line.WorkShop);
        PluEntities = GetPlus();
        
        Plu = new();
        PluNesting = new();
        KneadingModel = new();
        PluTemplate = new();
        
        ExternalDevices.SetupPrinter(Line.Printer.Ip, Line.Printer.Port, Line.Printer.Type);
        ExternalDevices.SetupScales(Line.DeviceComPort);
        Timer = new(_ => ExternalDevices.Scales.SendGetWeight(), null, TimeSpan.Zero, TimeSpan.FromSeconds(0.5));
    }
    
    private void NotifyStateChanged() => OnStateChanged?.Invoke();
}