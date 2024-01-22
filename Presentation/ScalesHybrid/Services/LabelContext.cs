using ScalesHybrid.Models;
using Ws.Services.Features.Line;
using Ws.Services.Features.Plu;
using Ws.StorageCore.Entities.SchemaRef.Lines;
using Ws.StorageCore.Entities.SchemaRef.Printers;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;
using Ws.StorageCore.Entities.SchemaScale.Templates;

namespace ScalesHybrid.Services;

public class LabelContext: IDisposable
{
    private ILineService LineService { get; }
    private IPluService PluService { get; }
    private LineContext LineContext { get; }
    
    public SqlLineEntity Line { get => LineContext.Line; }
    public SqlPrinterEntity Printer { get => LineContext.PrinterEntity; }
    public SqlPluEntity Plu { get; private set; } = new();
    public SqlTemplateEntity PluTemplate { get; private set; } = new();
    public SqlPluNestingFkEntity PluNesting { get; private set; } = new();
    public WeightKneadingModel KneadingModel { get; private set; } = new();
    public IEnumerable<SqlPluEntity> PluEntities { get; private set; } = [];
    
    public event Action? OnStateChanged;
    private Timer Timer { get; set; }

    public LabelContext(ILineService lineService, IPluService pluService, LineContext lineContext)
    {
        PluService = pluService;
        LineService = lineService;
        LineContext = lineContext;
        
        LineContext.OnLabelChanged += InitializeData;
        Timer = new(_ => LineContext.RequestScale(), null, TimeSpan.Zero, TimeSpan.FromSeconds(0.5));
    }

    public void InitializeData()
    {
        PluEntities = LineService.GetLineWeightPlus(Line);
        
        Plu = new();
        PluNesting = new();
        PluTemplate = new();
        KneadingModel = new();
        
        OnStateChanged?.Invoke();
    }
    
    public void ChangePlu(SqlPluEntity sqlPluEntity)
    {
        if (Plu.Equals(sqlPluEntity)) return;
        Plu = sqlPluEntity;
        PluTemplate = PluService.GetPluTemplate(Plu);
        IEnumerable<SqlPluNestingFkEntity> pluNestingEntities = PluService.GetPluNesting(Plu);
        PluNesting = pluNestingEntities.FirstOrDefault(item => item.IsDefault) ?? new();
        KneadingModel.KneadingCount = 1;
        if (Plu.IsCheckWeight)
            LineContext.ConnectScale();
        else
            LineContext.DisconnectScale();
        OnStateChanged?.Invoke();
    }

    public void Dispose()
    {
        LineContext.OnLabelChanged -= InitializeData;
        GC.SuppressFinalize(this);
    }
}