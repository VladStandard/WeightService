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

public class LineContext
{
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
        SqlLineEntity newLine = LineService.GetCurrentLine();
        LineEntities = LineService.GetLinesByWarehouse(newLine.Warehouse);
        PrinterEntity = newLine.Printer;
        ExternalDevices.SetupPrinter(PrinterEntity.Ip, PrinterEntity.Port, PrinterEntity.Type);
        ChangeLine(newLine);
    }

    public async Task ChangePlu(SqlPluEntity sqlPluEntity)
    {
        if (Plu.Equals(sqlPluEntity)) return;
        Plu = sqlPluEntity;
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

    public void ChangePluNesting(SqlPluNestingFkEntity sqlPluNestingEntity)
    {
        if (PluNesting.Equals(sqlPluNestingEntity)) return;
        PluNesting = sqlPluNestingEntity;
        NameFormatting.GetPluNestingFormattedName(sqlPluNestingEntity);
        NotifyStateChanged();
    }

    private IEnumerable<SqlPluEntity> GetPlus() => LineService.GetLineWeightPlus(Line);

    private IEnumerable<SqlPluNestingFkEntity> GetPluNestings() => PluService.GetPluNesting(Plu);

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