using ScalesHybrid.Models;
using ScalesHybrid.Utils;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Models.Entities.SchemaScale;
using Ws.Services.Features.Line;
using Ws.Services.Features.Plu;
using Ws.StorageCore.Helpers;

namespace ScalesHybrid.Services;

public class LineContext
{
    public LineEntity Line { get; private set; }
    public PluEntity Plu { get; private set; }
    public PrinterEntity PrinterEntity { get; private set; }
    public TemplateEntity PluTemplate { get; private set; }
    public PluNestingEntity PluNesting { get; set; }
    public WeightKneadingModel KneadingModel { get; set; }
    public IEnumerable<LineEntity> LineEntities { get; set; }
    public IEnumerable<PluEntity> PluEntities { get; set; }
    public IEnumerable<PluNestingEntity> PluNestingEntities { get; set; }
    public event Action OnStateChanged;
    private ILineService LineService { get; }
    private IPluService PluService { get; }
    private ExternalDevicesService ExternalDevices { get; }
    
    private Timer Timer { get; set; }

    public LineContext(ILineService lineService, IPluService pluService, ExternalDevicesService externalDevices)
    {
        LineService = lineService;
        PluService = pluService;
        ExternalDevices = externalDevices;
        InitData();
    }

    public void ChangeLine(LineEntity sqlLineEntity)
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
        LineEntity newLine = LineService.GetCurrentLine();
        LineEntities = LineService.GetLinesByWarehouse(newLine.Warehouse);
        PrinterEntity = newLine.Printer;
        ExternalDevices.SetupPrinter(PrinterEntity.Ip, PrinterEntity.Port, PrinterEntity.Type);
        ChangeLine(newLine);
    }

    public async Task ChangePlu(PluEntity pluEntity)
    {
        if (Plu.Equals(pluEntity)) return;
        Plu = pluEntity;
        PluTemplate = PluService.GetPluTemplate(Plu);
        PluNestingEntities = await Task.Run(GetPluNestings);
        PluNesting = PluNestingEntities.FirstOrDefault(item => item.IsDefault) ?? new();
        KneadingModel.KneadingCount = 1;
        if (Plu.IsCheckWeight)
            ExternalDevices.Scales.Connect();
        else
        {
            ExternalDevices.Scales.Disconnect();
        }
        NotifyStateChanged();
    }

    public void ChangePluNesting(PluNestingEntity pluNestingEntity)
    {
        if (PluNesting.Equals(pluNestingEntity)) return;
        PluNesting = pluNestingEntity;
        NameFormatting.GetPluNestingFormattedName(pluNestingEntity);
        NotifyStateChanged();
    }

    private IEnumerable<PluEntity> GetPlus() => LineService.GetLineWeightPlus(Line);

    private IEnumerable<PluNestingEntity> GetPluNestings() => PluService.GetPluNesting(Plu);

    private void InitData()
    {
        Line = LineService.GetCurrentLine();

        if (Line.IsExists)
        {
            Line.Version = VersionTracking.CurrentVersion;
            SqlCoreHelper.Instance.Update(Line);
        }
        
        PrinterEntity = Line.Printer;
        LineEntities = LineService.GetLinesByWarehouse(Line.Warehouse);
        PluEntities = GetPlus();
        
        Plu = new();
        PluNesting = new();
        KneadingModel = new();
        PluTemplate = new();
        
        ExternalDevices.SetupPrinter(Line.Printer.Ip, Line.Printer.Port, Line.Printer.Type);
        ExternalDevices.SetupScales(Line.ComPort);
        Timer = new(_ => ExternalDevices.Scales.SendGetWeight(), null, TimeSpan.Zero, TimeSpan.FromSeconds(0.5));
    }
    
    private void NotifyStateChanged() => OnStateChanged?.Invoke();
}